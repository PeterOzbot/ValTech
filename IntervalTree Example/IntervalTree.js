
// Interval Tree Implementation
// http://www.geeksforgeeks.org/interval-tree/
// Tree is used to check if annotation titles are overlapping

// The main function that searches a given interval i in a given
// Interval Tree.
function intervalSearch(root, interval) {
    // Base Case, tree is empty
    if (typeof root == 'undefined') return undefined;

    // If given interval overlaps with root
    if (doOVerlap(root.i, interval))
        return root.i;

    // If left child of root is present and max of left child is
    // greater than or equal to given interval, then i may
    // overlap with an interval is left subtree
    if (typeof root.left != 'undefined' && root.left.max >= interval.low)
        return intervalSearch(root.left, interval);

    // Else interval can only overlap with right subtree
    return intervalSearch(root.right, interval);
}

function doOVerlap(i1, i2) {
    if (i1.low <= i2.high && i2.low <= i1.high)
        return true;
    return false;
}
// A utility function to create a new Interval Search Tree Node
function newNode(interval) {
    var temp = new Object();
    temp.i = interval;
    temp.max = interval.high;
    return temp;
}

// A utility function to insert a new Interval Search Tree Node
// This is similar to BST Insert.  Here the low value of interval
// is used to maintain BST property
function insert(root, interval) {
    // Base case: Tree is empty, new node becomes root
    if (typeof root == 'undefined') return newNode(interval);

    // Get low value of interval at root
    var l = root.i.low;

    // If root's low value is smaller, then new interval goes to
    // left subtree
    if (interval.low < l)
        root.left = insert(root.left, interval);

        // Else, new node goes to right subtree.
    else
        root.right = insert(root.right, interval);

    // Update the max value of this ancestor if needed
    if (root.max < interval.high)
        root.max = interval.high;

    return root;
}
// Interval Tree Implementation end