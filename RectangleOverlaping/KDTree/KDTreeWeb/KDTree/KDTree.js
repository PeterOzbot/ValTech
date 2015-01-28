/// <summary>
/// A KDTree class represents the root of a variable-dimension KD-Tree.
/// </summary>
/// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
function KDTree() {
    // The array of locations.  [index][dimension]
    this.tPoints = [];
    // The array of data values. [index]
    this.tData = null;
    // The left and right children.
    this.pLeft = null;
    this.pRight = null;
    // The split dimension.
    this.iSplitDimension = 0;
    // The split value (larger go into the right, smaller go into left)
    this.fSplitValue = 0;
    // The min and max bound for this node.  All dimensions.
    this.tMinBound = null;
    this.tMaxBound = null;
    // Does this node represent only one point.
    this.bSinglePoint = true;

    // The number of items in this leaf node and all children.
    this.Size = 0;


    // Setup leaf elements.
    this.tPoints = [];
    this.tData = [];
}

// checks if KDTree is leaf
KDTree.prototype.IsLeaf = function () {
    return this.tPoints != null;
}


// Insert a new point into this leaf node.
// tPoint should be 4D array with coordinates
// value is object stored
KDTree.prototype.AddPoint = function (tPoint, kValue) {
    // Find the correct leaf node.
    var pCursor = this;
    while (!pCursor.IsLeaf()) {
        // Extend the size of the leaf.
        pCursor.ExtendBounds(tPoint);
        pCursor.Size++;

        // If it is larger select the right, or lower,  select the left.
        if (tPoint[pCursor.iSplitDimension] > pCursor.fSplitValue) {
            pCursor = pCursor.pRight;
        }
        else {
            pCursor = pCursor.pLeft;
        }
    }

    // Insert it into the leaf.
    pCursor.AddLeafPoint(tPoint, kValue);
}

// Insert the point into the leaf.
KDTree.prototype.AddLeafPoint = function (tPoint, kValue) {
    // Add the data point to this node.
    this.tPoints[this.Size] = tPoint;
    this.tData[this.Size] = kValue;
    this.ExtendBounds(tPoint);
    this.Size++;
}

// If the point lies outside the boundaries, return false else true.
KDTree.prototype.CheckBounds = function (tPoint) {
    for (var i = 0 ; i < 4 ; ++i) {
        if (tPoint[i] > this.tMaxBound[i]) return false;
        if (tPoint[i] < this.tMinBound[i]) return false;
    }
    return true;
}

// Extend this node to contain a new point.
KDTree.prototype.ExtendBounds = function (tPoint) {
    // If we don't have bounds, create them using the new point then bail.
    if (this.tMinBound == null) {
        this.tMinBound = [tPoint[0], tPoint[1], tPoint[2], tPoint[3]];
        this.tMaxBound = [tPoint[0], tPoint[1], tPoint[2], tPoint[3]];
        return;
    }

    // For each dimension.
    for (var i = 0 ; i < 4 ; ++i) {
        if (isNaN(tPoint[i])) {
            if (!isNaN(this.tMinBound[i]) || !isNaN(this.tMaxBound[i]))
                this.bSinglePoint = false;

            this.tMinBound[i] = Number.NaN;
            this.tMaxBound[i] = Number.NaN;
        }
        else if (this.tMinBound[i] > tPoint[i]) {
            this.tMinBound[i] = tPoint[i];
            this.bSinglePoint = false;
        }
        else if (this.tMaxBound[i] < tPoint[i]) {
            this.tMaxBound[i] = tPoint[i];
            this.bSinglePoint = false;
        }
    }
}

// Split this leaf node by creating left and right children, then moving all the children of
// this node into the respective buckets.
KDTree.prototype.SplitLeafNode = function () {
    // Create the new children.
    this.pRight = new KDTree();
    this.pLeft = new KDTree();

    // Move each item in this leaf into the children.
    for (var i = 0 ; i < this.Size ; ++i) {
        // Store.
        var tOldPoint = this.tPoints[i];
        var kOldData = this.tData[i];

        // If larger, put it in the right.
        if (tOldPoint[this.iSplitDimension] > this.fSplitValue)
            this.pRight.AddLeafPoint(tOldPoint, kOldData);

            // If smaller, put it in the left.
        else
            this.pLeft.AddLeafPoint(tOldPoint, kOldData);
    }

    // Wipe the data from this KDNode.
    this.tPoints = null;
    this.tData = null;
}



// Get the nearest neighbors to a point in the kd tree using a square euclidean distance function.
KDTree.prototype.NearestNeighbors = function (tSearchPoint) {
    return new NearestNeighbors(this, tSearchPoint);
}

// returns if rectangles overlap
KDTree.prototype.DoOverlaping = function (a1, a2, b1, b2) {

    if (a1.X < b2.X &&
        a2.X > b1.X &&
        a1.Y < b2.Y &&
        a2.Y > b1.Y) {
        return true;
    }
    else {
        return false;
    }
}

//-----------------------------------------------NearestNeighbors---------------------------------------------------------------------------------------------
/// <summary>
/// A NearestNeighbour iterator for the KD-tree which intelligently iterates and captures relevant data in the search space.
/// </summary>
function NearestNeighbors(pRoot, tSearchPoint) {
    // The point from which are searching in n-dimensional space.
    this.tSearchPoint = [];

    /// <summary>The tree nodes which have yet to be evaluated.
    this.pPending;
    /// <summary>The values which have been evaluated and selected.
    this.pEvaluated;
    // The root of the kd tree to begin searching from.
    this.pRoot = null;

    // The number of points we can still test before conclusion.
    this.iPointsRemaining;

    // Current value distance.
    this._CurrentDistance = -1;
    // Current value reference.
    this._Current = null;

    // Store the search point.
    this.tSearchPoint = tSearchPoint;

    // Store the point count, distance function and tree root.
    this.iPointsRemaining = Math.min(1, pRoot.Size);

    this.pRoot = pRoot;
    this._CurrentDistance = -1;

    // Create an interval heap for the points we check.
    this.pEvaluated = new IntervalHeap();

    // Create a min heap for the things we need to check.
    this.pPending = new MinHeap();
    this.pPending.Insert(0, pRoot);
}

// Check for the next iterator item.
NearestNeighbors.prototype.MoveNext = function () {
    // Bail if we are finished.
    if (this.iPointsRemaining == 0) {
        this._Current = null;
        return false;
    }

    // While we still have paths to evaluate.
    while (this.pPending.Size > 0 && (this.pEvaluated.Size == 0 || (this.pPending.MinKey() < this.pEvaluated.MinKey()))) {
        // If there are pending paths possibly closer than the nearest evaluated point, check it out
        var pCursor = this.pPending.Min();
        this.pPending.RemoveMin();

        // Descend the tree, recording paths not taken
        while (!pCursor.IsLeaf()) {
            var pNotTaken;

            // If the search point is larger, select the right path.
            if (this.tSearchPoint[pCursor.iSplitDimension] > pCursor.fSplitValue) {
                pNotTaken = pCursor.pLeft;
                pCursor = pCursor.pRight;
            }
            else {
                pNotTaken = pCursor.pRight;
                pCursor = pCursor.pLeft;
            }

            // Calculate the shortest distance between the search point and the min and max bounds of the kd-node.
            var fDistance = this.DistanceToRectangle(this.tSearchPoint, pNotTaken.tMinBound, pNotTaken.tMaxBound);

            // Only add the path we need more points or the node is closer than furthest point on list so far.
            if (this.pEvaluated.Size < this.iPointsRemaining || fDistance <= this.pEvaluated.MaxKey()) {
                this.pPending.Insert(fDistance, pNotTaken);
            }
        }

        // If all the points in this KD node are in one place.
        if (pCursor.bSinglePoint) {
            // Work out the distance between this point and the search point.
            var fDistance = this.Distance(pCursor.tPoints[0], this.tSearchPoint);

            // Add the point if either need more points or it's closer than furthest on list so far.
            if (this.pEvaluated.Size < this.iPointsRemaining || fDistance <= this.pEvaluated.MaxKey()) {
                for (var i = 0 ; i < pCursor.Size ; ++i) {
                    // If we don't need any more, replace max
                    if (this.pEvaluated.Size == this.iPointsRemaining)
                        this.pEvaluated.ReplaceMax(fDistance, pCursor.tData[i]);

                        // Otherwise insert.
                    else
                        this.pEvaluated.Insert(fDistance, pCursor.tData[i]);
                }
            }
        }

            // If the points in the KD node are spread out.
        else {
            // Treat the distance of each point seperately.
            for (var i = 0 ; i < pCursor.Size ; ++i) {
                // Compute the distance between the points.
                var fDistance = this.Distance(pCursor.tPoints[i], this.tSearchPoint);

                // Insert the point if we have more to take.
                if (this.pEvaluated.Size < this.iPointsRemaining)
                    this.pEvaluated.Insert(fDistance, pCursor.tData[i]);

                    // Otherwise replace the max.
                else if (fDistance < this.pEvaluated.MaxKey())
                    this.pEvaluated.ReplaceMax(fDistance, pCursor.tData[i]);
            }
        }
    }

    // Select the point with the smallest distance.
    if (this.pEvaluated.Size == 0)
        return false;

    this.iPointsRemaining--;
    this._CurrentDistance = this.pEvaluated.MinKey();
    this._Current = this.pEvaluated.Min();
    this.pEvaluated.RemoveMin();
    return true;
}

// Reset the iterator.
NearestNeighbors.prototype.Reset = function () {
    // Store the point count and the distance function.
    this.iPointsRemaining = Math.min(10, this.pRoot.Size);
    this._CurrentDistance = -1;

    // Create an interval heap for the points we check.
    this.pEvaluated = new IntervalHeap();

    // Create a min heap for the things we need to check.
    this.pPending = new MinHeap();
    this.pPending.Insert(0, this.pRoot);
}

// Find the squared distance between two n-dimensional points.
NearestNeighbors.prototype.Distance = function (p1, p2) {
    return this.CalculateDistance(p1, p2);
}

// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
NearestNeighbors.prototype.DistanceToRectangle = function (point, min, max) {
    // gets the points in min max collections which are relevant for this point
    // in min there is the lowest possible value for x1,y1,x2,y2
    // in max there is the highest possible value for x1,y1,x2,y2
    // we get the rectangle and then the distance to the closest possible rectangle(in the rectangle made from min max collection) 
    // is also the distance to the rectangle we get


    var minMaxRect = [];
    for (var i = 0 ; i < point.length ; ++i) {

        if (point[i] > max[i])
            minMaxRect[i] = max[i];
        else if (point[i] < min[i])
            minMaxRect[i] = min[i];
        else {
            minMaxRect[i] = point[i];
        }
    }
    return this.CalculateDistance(point, minMaxRect);
}
// calculates the shortest distance between the two rectangles
NearestNeighbors.prototype.CalculateDistance = function (p1, p2) {
    var dx = Math.max(p1[0], p2[0]) - Math.min(p1[2], p2[2]);
    var dy = Math.max(p1[1], p2[1]) - Math.min(p1[3], p2[3]);
    if (dx < 0) return Math.max(dy, 0);
    if (dy < 0) return dx;
    return Math.sqrt(dx * dx + dy * dy);
}

//-----------------------------------------------MinHeap---------------------------------------------------------------------------------------------
/// <summary>
/// A MinHeap is a smallest-first queue based around a binary heap so it is quick to insert / remove items.
/// </summary>
/// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
function MinHeap() {
    // The data array.  This stores the data items in the heap.
    this.tData = [];
    // The key array.  This determines how items are ordered. Smallest first.
    this.tKeys = [];
    // The amount of space in this queue.
    this.Capacity = 64;
    // The number of items in this queue.
    this.Size = 0;


}

// Insert a new element.
MinHeap.prototype.Insert = function (key, value) {
    // Insert new value at the end
    this.tData[this.Size] = value;
    this.tKeys[this.Size] = key;
    this.SiftUp(this.Size);
    this.Size++;
}

// Remove the smallest element.
MinHeap.prototype.RemoveMin = function () {
    if (this.Size == 0) {
        return;
    }

    this.Size--;
    this.tData[0] = this.tData[this.Size];
    this.tKeys[0] = this.tKeys[this.Size];
    this.tData[this.Size] = null;
    this.SiftDown(0);
}

// Get the data stored at the minimum element.
MinHeap.prototype.Min = function () {
    return this.tData[0];
}

// Get the key which represents the minimum element.
MinHeap.prototype.MinKey = function () {
    return this.tKeys[0];
}

// Bubble a child item up the tree.
MinHeap.prototype.SiftUp = function (iChild) {
    // For each parent above the child, if the parent is smaller then bubble it up.
    for (var iParent = (iChild - 1) / 2 ;
    iChild != 0 && this.tKeys[iChild] < this.tKeys[iParent];
    iChild = iParent, iParent = (iChild - 1) / 2) {
        var kData = this.tData[iParent];
        var dDist = this.tKeys[iParent];

        this.tData[iParent] = this.tData[iChild];
        this.tKeys[iParent] = this.tKeys[iChild];

        this.tData[iChild] = kData;
        this.tKeys[iChild] = dDist;
    }
}

// Bubble a parent down through the children so it goes in the right place.
MinHeap.prototype.SiftDown = function (iParent) {
    // For each child.
    for (var iChild = iParent * 2 + 1 ; iChild < this.Size ; iParent = iChild, iChild = iParent * 2 + 1) {
        // If the child is larger, select the next child.
        if (iChild + 1 < this.Size && this.tKeys[iChild] > this.tKeys[iChild + 1])
            iChild++;

        // If the parent is larger than the largest child, swap.
        if (this.tKeys[iParent] > this.tKeys[iChild]) {
            // Swap the points
            var pData = this.tData[iParent];
            var pDist = this.tKeys[iParent];

            this.tData[iParent] = this.tData[iChild];
            this.tKeys[iParent] = this.tKeys[iChild];

            this.tData[iChild] = pData;
            this.tKeys[iChild] = pDist;
        }

            // TODO: REMOVE THE BREAK
        else {
            break;
        }
    }
}
//-----------------------------------------------IntervalHeap---------------------------------------------------------------------------------------------
/// <summary>
/// A binary interval heap is double-ended priority queue is a priority queue that it allows
/// for efficient removal of both the maximum and minimum element.
/// </summary>
/// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
function IntervalHeap() {
    // The data array.  This stores the data items in the heap.
    this.tData = [];
    // The key array.  This determines how items are ordered. Smallest first.
    this.tKeys = [];
    // The amount of space in this queue.
    this.Capacity = 64;
    // The number of items in this queue.
    this.Size = 0;
}

// Get the data stored at the minimum element.
IntervalHeap.prototype.Min = function () {
    return this.tData[0];
}

// Get the key which represents the minimum element.
IntervalHeap.prototype.MinKey = function () {
    return this.tKeys[0];
}
// Get the data stored at the maximum element.
IntervalHeap.prototype.Max = function () {
    if (this.Size == 1) {
        return this.tData[0];
    }

    return this.tData[1];
}

// Get the key which represents the maximum element.
IntervalHeap.prototype.MaxKey = function () {
    if (this.Size == 1) {
        return this.tKeys[0];
    }

    return this.tKeys[1];
}

// Insert a new data item at a given key.
IntervalHeap.prototype.Insert = function (key, value) {
    // Insert the new value at the end.
    this.Size++;
    this.tData[this.Size - 1] = value;
    this.tKeys[this.Size - 1] = key;

    // Ensure it is in the right place.
    this.SiftInsertedValueUp();
}

// Remove the item with the smallest key from the queue.
IntervalHeap.prototype.RemoveMin = function () {

    // Remove the item by 
    this.Size--;
    this.tData[0] = this.tData[this.Size];
    this.tKeys[0] = this.tKeys[this.Size];
    this.tData[this.Size] = null;
    this.SiftDownMin(0);
}

/// Replace the item with the smallest key in the queue.
IntervalHeap.prototype.ReplaceMin = function (key, value) {

    // Add the data.
    this.tData[0] = value;
    this.tKeys[0] = key;

    // If we have more than one item.
    if (this.Size > 1) {
        // Swap with pair if necessary.
        if (this.tKeys[1] < key)
            this.Swap(0, 1);
        this.SiftDownMin(0);
    }
}

// Remove the item with the largest key in the queue.
IntervalHeap.prototype.RemoveMax = function () {
    // If we have one item, remove the min.
    if (this.Size == 1) {
        this.RemoveMin();
        return;
    }

    // Remove the max.
    this.Size--;
    this.tData[1] = this.tData[this.Size];
    this.tKeys[1] = this.tKeys[this.Size];
    this.tData[this.Size] = null;
    this.SiftDownMax(1);
}

/// Swap out the item with the largest key in the queue.
IntervalHeap.prototype.ReplaceMax = function (key, value) {
    if (this.Size == 1) {
        this.ReplaceMin(key, value);
        return;
    }

    this.tData[1] = value;
    this.tKeys[1] = key;
    // Swap with pair if necessary
    if (key < this.tKeys[0]) {
        this.Swap(0, 1);
    }
    this.SiftDownMax(1);
}

// Internal helper method which swaps two values in the arrays.
// This swaps both data and key entries.
IntervalHeap.prototype.Swap = function (x, y) {
    // Store temp.
    var yData = this.tData[y];
    var yDist = this.tKeys[y];

    // Swap
    this.tData[y] = this.tData[x];
    this.tKeys[y] = this.tKeys[x];
    this.tData[x] = yData;
    this.tKeys[x] = yDist;

    // Return.
    return y;
}

// Place a newly inserted element a into the correct tree position.
IntervalHeap.prototype.SiftInsertedValueUp = function () {
    // Work out where the element was inserted.
    var u = this.Size - 1;

    // If it is the only element, nothing to do.
    if (u == 0) {
    }

        // If it is the second element, sort with it's pair.
    else if (u == 1) {
        // Swap if less than paired item.
        if (this.tKeys[u] < this.tKeys[u - 1])
            this.Swap(u, u - 1);
    }

        // If it is on the max side, 
    else if (u % 2 == 1) {
        // Already paired. Ensure pair is ordered right
        var p = (u / 2 - 1) | 1; // The larger value of the parent pair
        if (this.tKeys[u] < this.tKeys[u - 1]) { // If less than it's pair
            u = this.Swap(u, u - 1); // Swap with it's pair
            if (this.tKeys[u] < this.tKeys[p - 1]) { // If smaller than smaller parent pair
                // Swap into min-heap side
                u = this.Swap(u, p - 1);
                this.SiftUpMin(u);
            }
        }
        else {
            if (this.tKeys[u] > this.tKeys[p]) { // If larger that larger parent pair
                // Swap into max-heap side
                u = this.Swap(u, p);
                this.SiftUpMax(u);
            }
        }
    }
    else {
        // Inserted in the lower-value slot without a partner
        var p = (u / 2 - 1) | 1; // The larger value of the parent pair
        if (this.tKeys[u] > this.tKeys[p]) { // If larger that larger parent pair
            // Swap into max-heap side
            u = this.Swap(u, p);
            this.SiftUpMax(u);
        }
        else if (this.tKeys[u] < this.tKeys[p - 1]) { // If smaller than smaller parent pair
            // Swap into min-heap side
            u = this.Swap(u, p - 1);
            this.SiftUpMin(u);
        }
    }
}

// Bubble elements up the min side of the tree.
IntervalHeap.prototype.SiftUpMin = function (iChild) {
    // Min-side parent: (x/2-1)&~1
    for (var iParent = (iChild / 2 - 1) & ~1;
    iParent >= 0 && this.tKeys[iChild] < this.tKeys[iParent];
    iChild = iParent, iParent = (iChild / 2 - 1) & ~1) {

        this.Swap(iChild, iParent);
    }
}

// Bubble elements up the max side of the tree.
IntervalHeap.prototype.SiftUpMax = function (iChild) {
    // Max-side parent: (x/2-1)|1
    for (var iParent = (iChild / 2 - 1) | 1 ;
    iParent >= 0 && this.tKeys[iChild] > this.tKeys[iParent];
    iChild = iParent, iParent = (iChild / 2 - 1) | 1) {
        this.Swap(iChild, iParent);
    }
}

// Bubble elements down the min side of the tree.
IntervalHeap.prototype.SiftDownMin = function (iParent) {
    // For each child of the parent.
    for (var iChild = iParent * 2 + 2 ; iChild < this.Size ; iParent = iChild, iChild = iParent * 2 + 2) {
        // If the next child is less than the current child, select the next one.
        if (iChild + 2 < this.Size && this.tKeys[iChild + 2] < this.tKeys[iChild]) {
            iChild += 2;
        }

        // If it is less than our parent swap.
        if (this.tKeys[iChild] < this.tKeys[iParent]) {
            this.Swap(iParent, iChild);

            // Swap the pair if necessary.
            if (iChild + 1 < this.Size && this.tKeys[iChild + 1] < this.tKeys[iChild]) {
                this.Swap(iChild, iChild + 1);
            }
        }
        else {
            break;
        }
    }
}

/// Bubble elements down the max side of the tree.
IntervalHeap.prototype.SiftDownMax = function (iParent) {
    // For each child on the max side of the tree.
    for (var iChild = iParent * 2 + 1 ; iChild <= this.Size ; iParent = iChild, iChild = iParent * 2 + 1) {
        // If the child is the last one (and only has half a pair).
        if (iChild == this.Size) {
            // CHeck if we need to swap with th parent.
            if (this.tKeys[iChild - 1] > this.tKeys[iParent])
                this.Swap(iParent, iChild - 1);
            break;
        }

            // If there is only room for a right child lower pair.
        else if (iChild + 2 == this.Size) {
            // Swap the children.
            if (this.tKeys[iChild + 1] > this.tKeys[iChild]) {
                // Swap with the parent.
                if (this.tKeys[iChild + 1] > this.tKeys[iParent])
                    this.Swap(iParent, iChild + 1);
                break;
            }
        }

            // 
        else if (iChild + 2 < this.Size) {
            // If there is room for a right child upper pair
            if (this.tKeys[iChild + 2] > this.tKeys[iChild]) {
                iChild += 2;
            }
        }
        if (this.tKeys[iChild] > this.tKeys[iParent]) {
            this.Swap(iParent, iChild);
            // Swap with pair if necessary
            if (this.tKeys[iChild - 1] > this.tKeys[iChild]) {
                this.Swap(iChild, iChild - 1);
            }
        }
        else {
            break;
        }
    }
}