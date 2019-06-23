# Wrapping the Generic Collection and adding some funcionalities 

This is not an extension of the class System.Collections.Generic. 
I made this class because I found myself wrapping the generic List<T> class inside another class many times, the reasons are new funcionalities not implemented by the standard List<T> class or to avoid the usage of Linq extensions (Garbage Collector). 
  
Basically, you have Add and Remove operations already checking and raising exceptions for adding null or duplicated elements to the collection and some auxiliar methods such as Shuffle or GetAndRemoveRandomElement(), these are very useful when you're  manipulating a collection of elements like cards, tiles, enemies, characters, etc. 
  
Inside the Editor folder you will find a test class used in my Unit Tests as well as some practice of Test Driven Development using the Collection<T> class:
  
![alt text](https://github.com/ycarowr/Tools/blob/master/Assets/Scripts/Tools/GenericCollection/Images/tdd%20collection.GIF)

