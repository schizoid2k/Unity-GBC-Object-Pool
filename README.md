# GBC Object Pool
## Developed by John Schumacher
### http://gamesbycandlelight.com
### @CandlelightGame (Twitter)

GBC Object Pool is a free Unity asset that allows you to use object pooling in your games. Object Pooling creates game objects prior to use, thereby eliminating the lag from repeated creation and destruction of objects over and over again.  Any prefab object can be pooled with this library.

You are given permission to use this library in your Unity projects.  I am going to add functionality as I need, and if you would like to contribute, please do so!  Keep in mind that I would like to keep this library somewhat simple, as there are several "more complex" poolers in the Asset Store.

## To Use This Asset
1. Download and import the **GBC Object Pooler** asset (located in the folder current-builds) into your Unity Project.
2. Drag the GBC Object Pooler prefab into your Unity scene.
3. Click the "Add Pool" button to add a new pool, a new empty pool will be created.
5. For each pool, drag in a prefab, enter the number of items you wish to create in the pool, and the name of the pooled items.
6. To delete a pool, click the associated delete button for the pool you wish to remove.
7. To create a file containing your constants, click on the "Create Constants File" button. This will create a constants file for the current scene. The constants files will be located in the "Constants" folder of the GBC Object Pooler asset.
8. In your code files, be sure to add the "using GBCObjectPooler" statement in order to call the pooler functions.

## How It Works

When the scene starts, the script will loop through the array of pool items, and will create the correct number of objects per pool.

Behind the scenes, a Queue is created for each object pool, and game objects are instantiated and placed in the queue.  The queue is then added to a Dictionary.

The script also creates an empty GameObject for every pool and the instantiated objects are children of that game object.  I like doing this because it is looks cleaner in the Inspector, and it is also easier to see what is going on during debugging.

## Using Constants
For the included methods, you can pass a string value, or constants. Using constants will allow you to pick a value from your editor via autocompletion.  Constants will also help prevent typeos when entering string values.

Since there can be more than one scene with the same pool name (for example "Bullets"), a file is generated for each scene.  When you click on "Create Constants File", a file will be created and will contain the name of the current scene.  All constant files, as well as the pooler library, is included in the GBCObjectPooler namespace.

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
I am adding features as I need them for my personal projects. Developing Unity Editor plugins is somewhat new to me, so I am sure there is room for improvement. If you would like to contribute (fixes or features), please feel free to clone this project and add your updates!  I would love to see what features we can add.

### Version History
2.5 07-May-2021 Added feature to create constants file(s)

2.1 27-Apr-2020 Fixed Singleton code in the Awake method

2.0 16-Dec-2019 Added Editor scripts for easier use.

1.0 13-Apr-2018 Initial Upload.
