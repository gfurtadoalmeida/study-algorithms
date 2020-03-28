# Algorithms & Data Structures Annotations

## Run Time Complexity

Notations

* Big-Oh (**O**): worst case.
* Big-Omega ($\Omega$): best case.
* Big-Theta ($\Theta$): average case.

Run Times

| Name               |     Time      | Example                                   |
| ------------------ | :-----------: | ----------------------------------------- |
| Constant           |     O(1)      | Adding an element at front of linked list |
| Logarithmic        |   O(log(n))   | Finding an element in a sorted array      |
| Linear             |     O(n)      | Finding an element in an unsorted array   |
| Linear Logarithmic | O(n * log(n)) | Merge Sort                                |
| Quadratic          |    O(n^2^)    | Shortest path between 2 nodes in a graph  |
| Cubic              |    O(n^3^)    | Matrix multiplication                     |
| Exponential        |    O(2^n^)    | Tower of Hanoi problem                    |

Data Structure Times

| Data Structure |  Insertion  |  Deletion   |  Searching  |
| -------------- | :---------: | :---------: | :---------: |
| Array          |    O(1)     |    O(1)     |    O(n)     |
| Linked List    |    O(n)     |    O(n)     |    O(n)     |
| Tree           |  O(log(n))  |  O(log(n))  |  O(log(n))  |
| Hash           | O(1) / O(n) | O(1) / O(n) | O(1) / O(n) |

## Calculating Run Time Complexity

### Rules

* During the calculations, constant numbers are discarded and transformed to 1 or 'n'.  
* At the final calculation, any O(1) is discarded.

### Interactive Algorithm

```csharp
// Find biggest number in an unsorted array
// n = array size
FindBiggestNumber(int[] array) {
    biggestNumber = array[0]; ----------------------------------- O(1)

    for (int i=0; i &lt; array.Length; i++) { --------- O(n) | -- O(n)
        if (array[i] > biggestNumber) { -- O(1) | -- O(1) |
            biggestNumber = array[i]; ---- O(1) |
        }
    }

    return biggestNumber; --------------------------------------- O(1)
}
```

**Equation:**  T(n) = O(1) + O(n) + O(1)  
**Run time:** O(n)

### Recursive Algorithm

```csharp
static int HIGHEST = int.MinValue; ---------------------------- O(1)

// Find biggest number in an unsorted array
// n = arraySize
FindBiggestNumber(int[] array, int arraySize) { --------------- T(n)
    if (n == -1) { ------- O(1) |------------------------------ O(1)
        return HIGHEST; -- O(1) |
    }
    else { ------------------------------------------ O(1) | -- O(1)
        if(array[arraySize] > HIGHEST) { -- O(1) | -- O(1) |
            HIGHEST = array[arraySize]; --- O(1) |
        }

        return FindBiggestNumber(array, arraySize-1); --------- T(n-1)
    }
}
```

**Equation:** T(n) = T(n-1) + O(1)  
**Base condition:** T(-1) = O(1) ... when array size == -1 the function returns.  

Equation 1: T(n)   = T(n-1) + 1  
Equation 2: T(n-1) = T((n-1)-1) + 1  
Equation 3: T(n-2) = T((n-2)-1) + 1  

T(n)   = T(n-1) + 1  
T(n-1) = (T((n-1)-1) + 1) + 1 = T(n-2) + 2  
T(n-2) = (T((n-2)-1) + 2) + 1  = T(n-3) + 3  
T(n)   = T(n-k) + k ... where k = array size  

T(-1)    = 1  
T(n-k)+k = 1  
T(n - -1) + -1 = 1  
T(n+1)-1 = 1  
T(n+1) = 2  
T(n) = n  

**Run time:** O(n)  

```csharp
// Find biggest number in a sorted array
// n = array size
BinarySearch (int number, int[] array, int start, int end) { --------------------- T(n)
    if(start == end) { ------------------------------------ O(1) | --------------- O(1)
        if(array[start] == number) { -- O(1) | -- O(1) | -- O(1) |
            return start; ------------- O(1) |         |
        } else { ---------------------- O(1) | -- O(1) |
            throw; -------------------- O(1) |
        }
    }

    int mid = FindMid(array, start end); ----------------------------------------- O(1)

    if (mid > number) { ---------------------------------- O(1)   | -- T(n/2) | -- T(n/2)
        return BinarySearch(number, array, start, mid); -- T(n/2) |           |
    } else if (mid <  number) { -------------------------- O(1)   | -- T(n/2) |
        return BinarySearch(number, array, mid, end); ---- T(n/2) |           |
    } else if (mid == number) { -------------------------- O(1)   | -- O(1)   |
        return mid; -------------------------------------- O(1)   |
    }
}
```

**Equation:** T(n) = T(n/2) + O(1)  
**Base condition:**  T(1) = O(1) ... when array size == 1 the function returns.  

Equation 1: T(n) = T(n/2) + 1  
Equation 2: T(n/2) = T(n/2/2) + 1 = T(n/4) + 1  
Equation 3: T(n/4) = T(n/4/4) + 1 = T(n/8) + 1  

T(n)   = T(n/2) + 1  
T(n/2) = (T(n/4) + 1) + 1 = T(n/4) + 2  
T(n/4) = (T(n/8) + 1) + 2 = T(n/8) + 3  
T(n)   = T(n/2^k) + k ... where k = array size  

T(1) = 1  
n/2^k = 1  
n = 2^k  
k = log(n)  
1 = log n  
T(1) = log(n)  
  
**Run time:** O(log(n))  

## Tree

Terminologies

* Tree: data type that simulates a hierarchical tree structure, with a root value and sub-trees of children with a parent node, represented as a set of linked nodes.
* Node: structure which may contain a value or condition, or represent a separate data structure.
* Edge: the connection between one node and another.
* Root: the top node in a tree, the prime ancestor.
* Child: a node directly connected to another node when moving away from the root, an immediate descendant.
* Parent: the converse notion of a child, an immediate ancestor.
* Siblings: a group of nodes with the same parent.
* Neighbor: parent or child.
* Descendant: a node reachable by repeated proceeding from parent to child. Also known as sub-child.
* Ancestor: a node reachable by repeated proceeding from child to parent.
* Leaf / External node: a node with no children.
* Branch node / Internal node: a node with at least one child.
* Degree: for a given node, it's number of children. A leaf is necessarily degree zero. The degree of a tree is the degree of its root.
* Degree of tree: the degree of the root.
* Path: a sequence of nodes and edges connecting a node with a descendant.
* Distance: the number of edges along the shortest path between two nodes.
* Depth: the distance between a node and the root.
* Level: 1 + the number of edges between a node and the root, i.e. (Depth + 1).
* Height: the number of edges on the longest downward path between a node and a leaf.
* Width: the number of nodes in a level.
* Breadth: the number of leaves.
* Height of tree: the height of the root node or the maximum level of any node in the tree.
* Forest: a set of n ≥ 0 disjoint trees.
* Sub-Tree: a tree T is a tree consisting of a node in T and all of its descendants in T.
* Ordered Tree: a rooted tree in which an ordering is specified for the children of each vertex.

### Binary Tree

A tree data structure in which each node has at most two children, which are referred to as the left child and the right child.

Types:  

* Strict Binary Tree (SBT): each node has either 2 children or none.
* Full Binary Tree: each non leaf node has two children and all leaf nodes are at the same level.
* Complete Binary Tree: all levels are completely filled except possibly the last level and the last level has all keys as left as possible.

Array Representation:

* Index zero is never used as it would complicated the formulas.
* Formulas:
  * Left child: parent position * 2
  * Right child: (parent position * 2) + 1

Traversal Modes:

```text
       1
     /   \
    2     3
   / \   / \
  4   5 6   7
 / \
8   9
```

* Depth First Search (DFS) [stack based]:
  * PreOrder:
    * Root -> Left side -> Right side.
    * Visit root first, then all left nodes until leaf, then return visiting the right nodes, applying the same logic.
    * Result: 1, 2, 4, 8, 9, 5, 3, 6, 7
  * InOrder:
    * Left side -> Root -> Right side.
    * Visit the left leaf on the left node, then return visiting first the root and then the right nodes, applying the same logic.
    * Result: 8, 4, 9, 2, 5, 1, 6, 3, 7
  * PostOrder:
    * Left side -> Right side -> Root.
    * Visit the left leaf on the left node, then return visiting first the right nodes, applying the same logic and then visit the root.
    * Result: 8, 9, 4, 5, 2, 6, 7, 3, 1
* Breadth First Search (BFS) [queue based]:
  * LevelOrder:
    * [Root -> Left side -> Right side] at same depth.
    * At the present depth, visit all nodes, from left to right, visiting first the root, left side and then right side, prior to moving on to the nodes at the next depth.
    * Result: 1, 2, 3, 4, 5, 6, 7, 8, 9

Run time: **O(n)**, regardless of implementation as it's not a balanced tree.

### Binary Search Tree

An ordered binary tree in which:

* The left sub-tree of a node has a key less than or equal to it's parent's key.
* The right sub-tree of a node has a key greater than it's parent's key.

Run time: from **O(log (n)) to O(n)**, regardless of implementation as it's not a balanced tree.

### Self-Balancing Binary Search Tree

A binary search tree that:

* Automatically keeps its height small.  
* The height of immediate sub-tree of any node differs by at most one (also called balance factor).  
* Empty height is considered -1.  

Run time: **O(log (n))**, regardless of implementation.

Types:

* AVL Tree
* B-Tree
* Red-Black Tree

### Binary Heap

A binary tree that:

* Is complete; all levels are completely filled except possibly the last level and the last level has all keys as left as possible.
* Only the root can be extracted and peeked.
* Has two types:
  * Min-Heap: the value of any given node must be less than or equal the value of its children.
  * Max-Heap: the value of any given node must be greater than the value of its children.

Run time:

* Array: **O(log(n))**
* Linked list: **from O(log(n)) to O(n)**

### Trie

A binary search tree typically used to store/search **strings** in space/time efficient way, in which nodes:

* Can store non-repetitive multiple characters.
* Stores the link to the next character of a string.
* Keeps track of the end of the string.

Used to solve problems like:

* Spelling checkers
* Auto complete

## Hashing

Method of sorting and indexing data using keys, where the keys are generated using a hash function.

Terminologies:

* Hash function:
  * Any function that, given an input, always generate the same output as a number.
  * The number returned **must fall** inside the array bounds.
* Key: input data given by the user.
* Hash value: the value returned by the hash function.
* Hash table: a data structure that implements an associative array abstract data type, a structure that can map keys to values.
* Collision: occurs when two different keys to a hash function produce the same hash value.

Collision Resolution Techniques:

| Name            | Cell Data   | Uses     |                                                                                                |
| --------------- | ----------- | -------- | ---------------------------------------------------------------------------------------------- |
| Direct chaining | Linked list | Good     | High deletion rate and/or unknown input size                                                   |
|                 |             | Bad      | High collisions creating long linked lists, no cache friendly                                  |
|                 |             | Resizing | Not a problem as it uses linked list                                                           |
| Open addressing | Key         | Good     | Input size is known and need to be cache friendly                                              |
|                 |             | Bad      | Deletion create holes needing resizing, may not find a slot even if the array have empty cells |
|                 |             | Resizing | New array creation and keys re-hashing                                                         |

* Direct chaining: array stores references to linked lists and values are added to the end of the linked list.
* Open addressing:
  * Linear probing: **index = (HashValue + A) mod N** ^1^
  * Quadratic probing: **index = (HashValue + A^2) mod N** ^1^
  * Double hashing: **index = [PrimaryHashFunc(key) + (A * SecondaryHashFunc(key))] mod N** ^1,2^

^1^ "A" is the attempt to find an empty cell, starting at 0 and "N" is the array size.
^2^ The second hash key must: never yield an index of zero, cycle through the whole table, be independent from the first hash function and be very fast to compute.

Open Addressing Techniques Matrix:

| Name              | Pros                                        | Cons                                 |
| ----------------- | ------------------------------------------- | ------------------------------------ |
| Linear probing    | Data locality and performance               | Data clustering                      |
| Quadratic probing | Avoids data clustering, although not immune | Not as fast as linear probing        |
| Double hashing    | No data clustering and less collision       | Slower as it uses two hash functions |

## Sorting

Based on:

* Space used:
  * In-place: does not require extra space for sorting.
  * Out-place: requires extra space for sorting.
* Stability:
  * Stable:
    * Does not change the sequence of similar items: 1,3,4A,2,4B => 1,2,3,4A,4B  
    * Useful when the sort key is not the entire identity of the item.
  * Unstable: changes the sequence of similar items: 1,3,4A,2,4B => 1,2,3,4B,4A

Example of Stable x Unstable Sort

In the following example, if the collection (sorted by name) were then sorted by gender using an unstable sort, the end result could be wrong because "Saul" could be positioned before "Rick"; the first sort (by name) would not be maintained in the second sort(by gender).

```text
 Stable (by name)        Stable (by gender)     Unstable (by gender)
| Name  | Gender |      | Name  | Gender |      | Name  | Gender |
| ***** | ****** |      | ***** | ****** |      | ***** | ****** |
| Norma |   2    |      | Paul  |   1    |      | Paul  |   1    |
| Paul  |   1    |  =>  | Rick  |   1    |  <>  | Saul  |   1    |
| Rick  |   1    |      | Saul  |   1    |      | Rick  |   1    |
| Ruth  |   2    |      | Norma |   2    |      | Norma |   2    |
| Saul  |   1    |      | Ruth  |   2    |      | Ruth  |   2    |
```

Sorting Algorithms Run Times

| Name      |     Time      | Space | Stable |
| --------- | :-----------: | :---: | :----: |
| Bubble    |    O(n^2^)    | O(1)  |  Yes   |
| Selection |    O(n^2^)    | O(1)  |   No   |
| Insertion |    O(n^2^)    | O(1)  |  Yes   |
| Bucket    | O(n * log(n)) | O(n)  |  Yes   |
| Merge     | O(n * log(n)) | O(n)  |  Yes   |
| Quick     | O(n * log(n)) | O(n)  |   No   |
| Heap      | O(n * log(n)) | O(1)  |   No   |

## Graphs

Graph is a pair of sets (V, E), where V is the set of vertices and E is the set of edges, connecting a pair of vertices.

```text
O-----O <- Vertex
   ^-- Edge
```

Terminologies

* Vertices: nodes of a graph.
* Edges: arcs that connect pairs of vertices
* Graphs:
  * Weighted: has weight (positive or negative) associated with each edge.
  * Unweighted: does not have a weight associated with any edge.
  * Directed: edges have a direction associated with them.
  * Undirected: edges does not have a direction associated with them.
  * Cyclic: has at least one loop.
  * Acyclic: dos not have loop.
  * Completed: all edges are known.
  * Uncompleted: not all edges are known.

Representation

* Adjacency matrix:
  * Square matrix where elements indicate whether pairs of vertices are adjacent or not.
  * Use for:
    * Complete or near complete graph.
* Adjacency list:
  * Collection of unordered linked lists where each list describes the set of neighbors of a vertex.
  * Use for:
    * Few number of edges.
    * Uncomplete graph.

### Traversal Techniques

* Breadth First Search (BFS): visits the neighbors at the current level before moving to the next level.  
* Depth First Search (DFS): visits all nodes as far as possible along each edge before backtracking.

BFS

```text
V2---V3---\
|          \
V5---V6---V10  =>  V2, V3, V5, V6, V10, V8, V9
|          /
V8---V9---/

while (all vertices are not visited) {
  enqueue(anyVertex);

  while (queue is not empty) {
    vertexToVisit = dequeue();

    if (vertexToVisit is not visited) {
      enqueue(all adjacent unvisited vertices of vertexToVisit);
    }
  }
}
```

DFS

```text
V2---V3---\
|          \
V5---V6---V10  =>  V2, V5, V8, V9, V6, V3, V10
|          /
V8---V9---/

while (all vertices are not visited) {
  push(anyVertex);

  while (stack is not empty) {
    vertexToVisit = pop();

    if (vertexToVisit is not visited) {
      push(all adjacent unvisited vertices of vertexToVisit);
    }
  }
}
```

BFS x DFS

|                  |            BFS             |                  DFS                  |
| ---------------- | :------------------------: | :-----------------------------------: |
| Traversal mode   |         In breadth         |               In depth                |
| Data structure   |           Queue            |                 Stack                 |
| Time complexity  |          O(V + E)          |               O(V + E)                |
| Space complexity |          O(V + E)          |               O(V + E)                |
| When to use      | Target is buried very deep | Target is close to the starting point |

### Topological Sort

Sort actions in such a way that if there is a dependency of one action on another, then the dependent action always comes later than its parent action; sort actions in order of execution.

```text
Sort(graph) {
  for (all the vertices in the graph) {
    if (vertex is not visited) {
      Visit(vertex);
    }
  }

  pop();
}

Visit(currentNode) {
  for (each neighbor of currentNode) {
    if (neighbor is not visited) {
      TopologicalVisit(neighbor);

      mark node as visited;
      push(node);
    }
  }
}
```

Run time: **O(V + E)**
Space: **O(E)**

### Single Source Shortest Path (SSSP)

Given a source vertex find the path with minimum distance (or sum of weights) to a target vertex.

|                    |       BFS       |    Dijkstra     | Bellman Ford |
| ------------------ | :-------------: | :-------------: | :----------: |
| Time complexity    |     O(V^2^)     |     O(V^2^)     |   O(V * E)   |
| Space complexity   |      O(E)       |      O(V)       |     O(V)     |
| Does not work with | Weighted graphs | Negative cycles |      -       |
| Unweighted graph   |     Optimal     |        -        |      -       |
| Weighted graph     |        -        |     Optimal     |      -       |
| Negative cycles    |        -        |        -        |   Optimal    |

Breadth First Search (BFS)

```text
create a parent reference in each node;

enqueue(sourceVertex);

while (queue is not empty) {
  currentVertex = dequeue();

  for (each neighbor in currentVertex) {
    if (neighbor is not visites) {
      enqueue(adjcenVertex);

      set neighbor parent to currentVertex;
      set currentVertex as visited;
    }
  }
}
```

Dijkstra

```text
set the distance of all vertices to infinite and source vertex to 0;
save all vertices in minHeap;

while (minHeap not empty) {
  currentVertex = extract top from minHeap;

  for (each neighbor in currentVertex) {
    if (currentVertex's distance + currentEdge < neighbor's distance) {
      set neighbor's distance to currentVertex's distance + currentEdge;
      set neighbor's parent as currentVertex;

      // refresh min heap
      remove neighbor from minHeap;
      add neighbor to min heap;
    }
  }
}
```

Bellman Ford

```text
set the distance of all vertices to infinite and source vertex to 0;

for (1 to V-1) {
  for (each vertex in graph) {
    for (each neighbor in currentVertex) {
      if (neighbor's distance > currentVertex's distance + neighbor's weight) {
        set neighbor's distance to currentVertex's distance + neighbor's weight;
        set neighbor's parent as currentVertex;
      }
    }
  }
}

for (each vertex in graph) {
  for (each neighbor in currentVertex) {
     if (neighbor's distance > currentVertex's distance + neighbor's weight) {
       throw an error as the graph has a negative cycle;
  }
}
```

### All Pair Shortest Path (APSP)

Find the path with minimum distance (or sum of weights) between every vertex and every other vertices.

|                    |       BFS       |    Dijkstra     | Bellman Ford | Floyd Warshall  |
| ------------------ | :-------------: | :-------------: | :----------: | :-------------: |
| Time complexity    |     O(V^2^)     |     O(V^2^)     |   O(V * E)   |     O(V^3^)     |
| Space complexity   |      O(E)       |      O(V)       |     O(V)     |     O(V^2^)     |
| Unweighted graph   |     Optimal     |       Yes       |     Yes      |       Yes       |
| Weighted graph     |       No        |     Optimal     |     Yes      |       Yes       |
| Negative cycles    |       No        |       No        |   Optimal    |       No        |

For BFS, Dijkstra and Bellman Ford, just run the algorithm from every vertex to every other vertices.

Floyd Warshall

```text
initialize a table with size V * V and set every cell to infinite;
copy all distances from the graph to the table;

for (k = 0 to vertices - 1) {
  for (i = 0 to vertices - 1) {
    for (j = 0 to vertices - 1) {
      if (table[i][j] > table[i][k] + table[k][j]) {
        table[i][j] > table[i][k] + table[k][j];
      }
    }
  }

return table;
}
```

### Minimum Spanning Tree (MST)

Subset of the edges of a connected, weighted, undirected graph that:

* Connects all the vertices together.
* Does not have cycles.
* Has minimum total weight.

Disjoint Set  

Data structure that keeps track of 'set of elements' that are partitioned into a number of disjoint (no items in common, no intersection) and non-overlapping sets.  

|                  |    Kruskal    |     Prim      |
| ---------------- | :-----------: | :-----------: |
| Time complexity  | O(E * log(E)) | O(E * log(V)) |
| Space complexity |   O(V + E)    |     O(V)      |

Kruskal

```text
for (each vertex) {
  makeset(vertex);
}

sort each edge in ascending order by weight;

for (each edge[source, target]) {
  if (findset(source) != finset(target)) {
    union(findset(source), findset(target));

    cost = cost + weight(edge(source, target));
  }
}
```

Prim

```text
create a prioriy queue Q;
insert all vertices into Q in such a way that the source vertex is 0 and all other vertices are infinite;

while (Q is not empty) {
  currentVertex = dequeue();

  for (each unvisited neighbor in currentVertex) {
    if (neighbor's weight > currentVertex's edge weight) {
      set neighbor's distance to currentVertex's edge weight;
      set neighbor's parent to currentVertex;
    }

    mark neighbor as visited;
  }
}
```

## Algorithm Techniques

### Magic Framework

```text
[Problem Statement]
        |
[Is greedy choice applicabe?]-(yes)-[Apply greedy choice]
        | (no)
[Need optimal substructure?]-(yes)-[Overlaping subproblem?]-(yes)-[Apply dynamic programming]
        | (no)                                | (no)
[Apply brute force]               [Apply divide and conquer]
```

### Greedy Algorithm

Algorithmic paradigm that:

* Builds up a solution piece by piece.
* Chooses the next piece that offers the most obvious and immediate result.
* Choosing a locally optimal solution also leads to a global optimal solution; an item will be processed only once or be postponed (and processed only once).

Common algorithms:

* Insertion sort
* Selection sort
* Topological sort
* Kruskal
* Prim

Common problems:

* Activity selection
* Coin change
* Fraction knapsack

### Divide and Conquer

Algorithmic paradigm that:

* Breaks down a problem into subproblems of similar type until they can be solved directly.
* Combine the subproblems solution to get the final solution.
* Uses recursion.

Common algorithms:

* Merge sort
* Quick sort
* Binary search

Common problems:

* Fibonacci
* Number factor
* String conversion
* Zero-one knapsack
* Longest common subsequence
* Longest palindromic subsequence/substring
* Minimum cost to reach last cell in a 2D array
* Number of paths to reach last cell with a given cost in a 2D array

### Dynamic Programming

Algorithmic paradigm that:

* Breaks down a problem into a collection of simpler subproblems, solving each subproblem just once and storing the solution.
* The next time the same subproblem occurs, instead of re-computing the solution it just looks up for the stored solution.
* It's an optimization of the divide and conquer technique.
