# unity-react
An example setup to run React on top of Unity for Android.

<img src="https://media.giphy.com/media/l0HUokdABHHFv5mMw/giphy.gif" width="150" />

## How to use
1. Have an Android (virtual) device connected
2. Open the Unity project with Unity (2017.x or higher will do)
3. Run Build/Run Android, the first time this can take up to a couple minutes
4. Open app on device

## What it does
When running the build, the Unity project will be exported as a Gradle project. The src and libs will be copied over to another Gradle project folder, React/UnityReactExport. Then, React Native is build with UnityReactExport as a dependency project.

When opening the app, a custom layout is created with both the React and Unity layouts as childs. It supports hot reloading of the JS content.

## Why?
Mostly for fun. But if you find yourself in a situation in which this makes sense, I hope this example helps.

## Any issues or questions? 
Please let me know through marijn@marijnzwemmer.com
