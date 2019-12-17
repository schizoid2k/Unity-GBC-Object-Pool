# GBC Object Pool
## Developed by John Schumacher
### http://gamesbycandlelight.com
### @CandlelightGame (Twitter)

GBC Object Pool is a free Unity asset that allows you to use object pooling in your games. Object Pooling creates game objects prior to use, thereby eliminating the lag from repeated creation and destruction of objects over and over again.  Any prefab object can be pooled with this library.

You are given permission to use this library in your Unity projects.  I am going to add functionality as I need, and if you would like to contribute, please do so!  Keep in mind that I would like to keep this library somewhat simple, as there are several "more complex" poolers in the Asset Store.

## To Use This Asset
1. Download and import the **GBC Object Pool** asset (located in the folder current-builds) into your Unity Project.
2. Drag the GBC Object Pooler prefab into your Unity scene.
3. Click the "Add Pool" button to add a new pool, a new empty pool will be created.
5. For each pool, drag in a prefab, enter the number of items you wish to create in the pool, and the name of the pooled items.
6. To delete a pool, click the associated delete button for the pool you wish to remove.

## How It Works

When the scene starts, the script will loop through the array of pool items, and will create the correct number of objects per pool.

Behind the scenes, a Queue is created for each object pool, and game objects are instantiated and placed in the queue.  The queue is then added to a Dictionary.

The script also creates an empty GameObject for every pool and the instantiated objects are children of that game object.  I like doing this because it is looks cleaner in the Inspector, and it is also easier to see what is going on during debugging.

## GBC Object Pool Methods

### GetPooledObject(string PoolName)
This will return an item from the pool specified by *PoolName*.  
If the pool does not exist, or if there are no more unused items in the pool, *null* is returned.

### ReturnToPool(GameObject object, string PoolName)  
This will place the specified game object back into the pool identified in *PoolName*, and will disable it, so it can be reused.

### ItemsInPool(string PoolName)  
This will return the number of items still available in the pool specified in *PoolName*.  
If the pool does not exist, *null* is returned.

### How About Contributing?
If you would like to contribute, please feel free to clone and add your updates!  I would love to see what features we can add.

### Version History
1.0 13-Apr-2018 Initial Upload.

2.0 16-Dec-2019 Added Editor scripts for easier use.
