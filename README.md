# GBC Object Pool
## Originally Written by John Schumacher
### http://gamesbycandlelight.com

GBC Object Pool is a free Unity asset that allows you to use object pooling in your games. Object Pooling creates game objects prior to use, thereby eliminating the lag from repeated creation and destruction of objects over and over again.  Any prefab can be pooled.

You are given permission to use this library in your Unity projects.  I am going to add functionality as I need, and if you would like to contribute, please do so!  I look forward to your improvements.  Keep in mind that I would like to keep this library somewhat simple, as there are several "more complex" poolers in the Asset Store, but there are still some nice features that can be added.

## To Use This Asset
1. Download the UnityPackage from the current-build folder and import this asset into your Unity Project
2. Drag the GBC Object Pooler prefab into your Unity scene
3. Click on the prefab in your scene, and in the Inspector you will notice a script named GBCObjectPooler.
4. Enter the number of pools you wish to create.
5. For each pool, drag in a prefab, enter the number of items you wish to create in the pool, and the name of the pooled items.

## How It Works

When the scene starts, the script will loop through the array of pool items, and will create the correct number of objects per pool.

Behind the scenes, a Queue is created for each object pool, and game objects are instantiated and placed in the queue.  The queue is then added to a Dictionary.

The script also creates an empty GameObject for every pool and the instantiated objects are children of that empty.  I like doing this because it is looks cleaner in the Inspector, and it is also easier to see what is going on during debugging.

## Using Objects

### GetPooledObject(string poolName)
This will return an item from the pool specified.  
If the pool does not exist, or if there are no more items in the pool, _null_ is returned.

### ReturnToPool(GameObject object, string poolName)  
This will place the specified item back into the correct pool, and will disable it.

### ItemsInPool(string poolName)  
This will return the number of items still available in the specified pool.  
If the pool does not exist, _null_ is returned.
