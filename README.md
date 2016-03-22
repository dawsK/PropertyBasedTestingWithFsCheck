# Property-based testing with F# and FsCheck

F# and FsCheck are the perfect toolset when it comes to defining and testing properties for your .NET code! Most unit tests that we write today are example-based. Given an example set of data, we assert that our system under test produces the expected result. But how many examples do we have to test to be confident in our system? With property-based tests, we can define characteristics of our code that will always hold true, and then throw globs of random and edge-case data at it to prove it!

See how F# and FsCheck work with your favorite unit testing framework to make property-based testing straight-forward, readable, and effective.

## To Run

This slide deck uses [FsReveal](https://github.com/fsprojects/FsReveal). To run, open a console/terminal in the PropertyBasedTestingWithFsCheck folder.

If you're using Windows run

```
build.cmd
```

If you're using a Mac run

``` 
./build.sh
```

This will start up a local HTTP server that hosts the slides and also fire up your browser to the right location to view them.

