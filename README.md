ValTech
=======

#### Peak Detection

Algorithm for detecting which point is peak in a given list of points. Idea taken from [Simple Algorithms for Peak Detection in Time-Series - Girish Keshav Palshikar)](http://sites.google.com/site/girishpalshikar/Home/mypublications/SimpleAlgorithmsforPeakDetectioninTimeSeriesACADABAI_2009.pdf).
<br> Sample app generates some curve and detects peaks on it. Demonstration uses [D3.js](http://d3js.org/) library. Detection can be calibrated with few parameters.

#### Rectangle Overlaping

Using custom implementation of [K-D Tree](http://en.wikipedia.org/wiki/K-d_tree) to check if new rectangle does not overlap any existing one.
<br>Sample app generates random rectangle in given 2D space. For each new one checks if it overlaps any previously generated. if it overlpas its not drawn.

#### Web page image capture

Tool to create images of web pages. Tool gets URL and creates image of the page on that URL.
<br>For image capture [PhantomJS](http://phantomjs.org/) is used.

#### Async Data & UI Virtualization

Custom collection for data virtualization with support for UI virtualization also.
<br>New batch (page) of elements is loaded asynchronously.

#### Interval tree

Implemented data structure Interval Tree for checking if intervals overlap.
<br>Implemented in Javascript.
<br>Explanation can be found [here](http://www.geeksforgeeks.org/interval-tree/).

#### Animating Panel

Two custom WPF panels. One panel animates elements added to it another animates elements on mouse over.

#### WPF 3D Animation

3D animation implemented in WPF. Control is rotated depending where mouse cursor is.
<br>Example of binding ICommand to control events in XAML with behaviors.

#### Tree Builder

Creates tree structure (Parent, Children) of nodes from flat unordered list.
<br>Performance is O(n) with the power of references and at the expense of memory.

#### FacebookBusyIndicator

Busy indicator similar to the facebook's.

#### Slovenia Holidays

Function checks if the input date is a Slovenian day off holiday.
<br>Most holidays are hard-coded, easter is calculated.
Based on [Revealing the divine mathematics of a holiday](http://www.whydomath.org/Reading_Room_Material/ian_stewart/2000_03.html).

#### CustomScrollbarTableLayoutPanel
WinForms custom scrollbar integrated with TableLayoutPanel. Custom scrollbar can be skinned as desired.
<br> Idea taken from https://www.codeproject.com/Articles/14801/How-to-skin-scrollbars-for-Panels-in-C