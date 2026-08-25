# LoopedList

LoopedList is a custom class that implements a type of list that is looped. This means that after the last element, it gives a link to the first element, and the cycle repeats itself. It is implemented as a generic class, meaning that it can store any type of data.

## Usage
To use LoopedList, you need to first create an instance of the class. There are three constructors available:

```csharp

// Creates an empty LoopedList
var myList = new LoopedList<int>();

// Creates a LoopedList with elements from an IEnumerable
var myList = new LoopedList<int>(myIEnumerable);

// Creates a LoopedList with a specified capacity
var myList = new LoopedList<int>(capacity);

```

Once you have created an instance of the class, you can add elements to it using the standard ```Add``` method, or you can initialize it with a collection when creating it.


```csharp
// Adding elements to LoopedList
myList.Add(1);
myList.Add(2);
myList.Add(3);

// Initializing LoopedList with a collection
var myList = new LoopedList<int> { 1, 2, 3 };
```

You can access elements of the LoopedList using the standard index notation:
```csharp
// Accessing elements of LoopedList
var firstElement = myList[0];
var secondElement = myList[1];
var lastElement = myList[myList.Count - 1];
```

Additionally, the LoopedList class has a new this indexer which implements an infinite indexator. This is useful for iterating through the list using a for loop:
```csharp
// Using infinite indexator for loop iteration
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(myList[i]);
}

```

Finally, LoopedList also implements an IEnumerable interface, meaning that you can use it in a foreach loop:

```csharp
// Using LoopedList in a foreach loop
foreach (var element in myList)
{
    Console.WriteLine(element);
}
```


## Special Features
LoopedList has a few special features to help with edge cases:

### CancellationToken
LoopedList has a CancellationToken property that can be used to break out of an infinite loop in a foreach loop. This is useful if you don't want the loop to continue indefinitely. You can toggle the token like this:


### GetLoopedIndex
LoopedList has a GetLoopedIndex method that is used to get the real index that displays the index of the list in the LoopedList. This method is useful for when you need to access the real index of an element in the list.
