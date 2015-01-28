using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTreeSimpleLibrary {
    /// <summary>
    /// A NearestNeighbour iterator for the KD-tree which intelligently iterates and captures relevant data in the search space.
    /// </summary>
    /// <typeparam name="KDRectangle">The type of data the iterator should handle.</typeparam>
    public class NearestNeighbour : IEnumerator {
        /// <summary>The point from which are searching in n-dimensional space.</summary>
        private double[] tSearchPoint;

        /// <summary>The tree nodes which have yet to be evaluated.</summary>
        private MinHeap pPending;
        /// <summary>The values which have been evaluated and selected.</summary>
        private IntervalHeap pEvaluated;
        /// <summary>The root of the kd tree to begin searching from.</summary>
        private Node pRoot = null;

        /// <summary>The number of points we can still test before conclusion.</summary>
        private int iPointsRemaining;


        /// <summary>Current value distance.</summary>
        private double _CurrentDistance = -1;
        /// <summary>Current value reference.</summary>
        private KDRectangle _Current = default(KDRectangle);

        /// <summary>
        /// Construct a new nearest neighbour iterator.
        /// </summary>
        /// <param name="pRoot">The root of the tree to begin searching from.</param>
        /// <param name="tSearchPoint">The point in n-dimensional space to search.</param>
        public NearestNeighbour(Node pRoot, double[] tSearchPoint) {
            // Check the dimensionality of the search point.
            if (tSearchPoint.Length != 4)
                throw new Exception("Dimensionality of search point and kd-tree are not the same.");

            // Store the search point.
            this.tSearchPoint = new double[tSearchPoint.Length];
            Array.Copy(tSearchPoint, this.tSearchPoint, tSearchPoint.Length);

            // Store the point count, distance function and tree root.
            this.iPointsRemaining = Math.Min(10, pRoot.Size);
         
            this.pRoot = pRoot;
            _CurrentDistance = -1;

            // Create an interval heap for the points we check.
            this.pEvaluated = new IntervalHeap();

            // Create a min heap for the things we need to check.
            this.pPending = new MinHeap();
            this.pPending.Insert(0, pRoot);
        }

        /// <summary>
        /// Check for the next iterator item.
        /// </summary>
        /// <returns>True if we have one, false if not.</returns>
        public bool MoveNext() {
            // Bail if we are finished.
            if (iPointsRemaining == 0) {
                _Current = default(KDRectangle);
                return false;
            }

            // While we still have paths to evaluate.
            while (pPending.Size > 0 && (pEvaluated.Size == 0 || (pPending.MinKey < pEvaluated.MinKey))) {
                // If there are pending paths possibly closer than the nearest evaluated point, check it out
                Node pCursor = pPending.Min;
                pPending.RemoveMin();

                // Descend the tree, recording paths not taken
                while (!pCursor.IsLeaf) {
                    Node pNotTaken;

                    // If the seach point is larger, select the right path.
                    if (tSearchPoint[pCursor.iSplitDimension] > pCursor.fSplitValue) {
                        pNotTaken = pCursor.pLeft;
                        pCursor = pCursor.pRight;
                    }
                    else {
                        pNotTaken = pCursor.pRight;
                        pCursor = pCursor.pLeft;
                    }

                    // Calculate the shortest distance between the search point and the min and max bounds of the kd-node.
                    double fDistance = this.DistanceToRectangle(tSearchPoint, pNotTaken.tMinBound, pNotTaken.tMaxBound);

                    // If it is greater than the threshold, skip.
                    if (-1 >= 0 && fDistance > -1) {
                        //pPending.Insert(fDistance, pNotTaken);
                        continue;
                    }

                    // Only add the path we need more points or the node is closer than furthest point on list so far.
                    if (pEvaluated.Size < iPointsRemaining || fDistance <= pEvaluated.MaxKey) {
                        pPending.Insert(fDistance, pNotTaken);
                    }
                }

                // If all the points in this KD node are in one place.
                if (pCursor.bSinglePoint) {
                    // Work out the distance between this point and the search point.
                    double fDistance = this.Distance(pCursor.tPoints[0], tSearchPoint);

                    // Skip if the point exceeds the threshold.
                    // Technically this should never happen, but be prescise.
                    if (-1 >= 0 && fDistance >= -1)
                        continue;

                    // Add the point if either need more points or it's closer than furthest on list so far.
                    if (pEvaluated.Size < iPointsRemaining || fDistance <= pEvaluated.MaxKey) {
                        for (int i = 0 ; i < pCursor.Size ; ++i) {
                            // If we don't need any more, replace max
                            if (pEvaluated.Size == iPointsRemaining)
                                pEvaluated.ReplaceMax(fDistance, pCursor.tData[i]);

                            // Otherwise insert.
                            else
                                pEvaluated.Insert(fDistance, pCursor.tData[i]);
                        }
                    }
                }

                // If the points in the KD node are spread out.
                else {
                    // Treat the distance of each point seperately.
                    for (int i = 0 ; i < pCursor.Size ; ++i) {
                        // Compute the distance between the points.
                        double fDistance = this.Distance(pCursor.tPoints[i], tSearchPoint);

                        // Skip if it exceeds the threshold.
                        if (-1 >= 0 && fDistance >= -1)
                            continue;

                        // Insert the point if we have more to take.
                        if (pEvaluated.Size < iPointsRemaining)
                            pEvaluated.Insert(fDistance, pCursor.tData[i]);

                        // Otherwise replace the max.
                        else if (fDistance < pEvaluated.MaxKey)
                            pEvaluated.ReplaceMax(fDistance, pCursor.tData[i]);
                    }
                }
            }

            // Select the point with the smallest distance.
            if (pEvaluated.Size == 0)
                return false;

            iPointsRemaining--;
            _CurrentDistance = pEvaluated.MinKey;
            _Current = pEvaluated.Min;
            pEvaluated.RemoveMin();
            return true;
        }

        /// <summary>
        /// Reset the iterator.
        /// </summary>
        public void Reset() {
            // Store the point count and the distance function.
            this.iPointsRemaining = Math.Min(10, pRoot.Size);
            _CurrentDistance = -1;

            // Create an interval heap for the points we check.
            this.pEvaluated = new IntervalHeap();

            // Create a min heap for the things we need to check.
            this.pPending = new MinHeap();
            this.pPending.Insert(0, pRoot);
        }

        /// <summary>
        /// Return the current value referenced by the iterator as an object.
        /// </summary>
        object IEnumerator.Current {
            get { return _Current; }
        }

        /// <summary>
        /// Return the distance of the current value to the search point.
        /// </summary>
        public double CurrentDistance {
            get { return _CurrentDistance; }
        }

        /// <summary>
        /// Return the current value referenced by the iterator.
        /// </summary>
        public KDRectangle Current {
            get { return _Current; }
        }

        /// <summary>
        /// Find the squared distance between two n-dimensional points.
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>The n-dimensional squared distance.</returns>
        private double Distance(double[] p1, double[] p2) {
            double fSum = 0;
            for (int i = 0 ; i < p1.Length ; i++) {
                double fDifference = (p1[i] - p2[i]);
                fSum += fDifference * fDifference;
            }
            return fSum;
        }

        /// <summary>
        /// Find the shortest distance from a point to an axis aligned rectangle in n-dimensional space.
        /// </summary>
        /// <param name="point">The point of interest.</param>
        /// <param name="min">The minimum coordinate of the rectangle.</param>
        /// <param name="max">The maximum coorindate of the rectangle.</param>
        /// <returns>The shortest squared n-dimensional squared distance between the point and rectangle.</returns>
        private double DistanceToRectangle(double[] point, double[] min, double[] max) {
            double fSum = 0;
            double fDifference = 0;
            for (int i = 0 ; i < point.Length ; ++i) {
                fDifference = 0;
                if (point[i] > max[i])
                    fDifference = (point[i] - max[i]);
                else if (point[i] < min[i])
                    fDifference = (point[i] - min[i]);
                fSum += fDifference * fDifference;
            }
            return fSum;
        }
    }
}
