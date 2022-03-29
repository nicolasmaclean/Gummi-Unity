# Welcome to Gummi

Gummi is a library for creating games in the Unity game Engine built by me! Nick Maclean. This project contains resources useful for prototyping or just about any project.

The C# library is sorted into 2 sections: core and the rest.

The Core lies in the `Gummi.Core` namespace and provides functionality core to the entire Gummi library. The rest of the logic is contained in sub-namespaces to `Gummi`.

# Getting Started

Unfortunately as of writing this, Gummi has not been published to upm (Unity Package Manager). This means adding this package to your project can be done in one of two ways: downloading this repo and dropping it into your project or by updating the manifest.json in your project.

The following line `"com.nicolasmaclean.gummi": "https://github.com/nicolasmaclean/Gummi-Unity.git"` may be added to dependencies of your project in the manifest.json file located at `your-project/Packages/manifest.json`. Unity will automatically download the latest version of the repo into your project as a package separate from your `Assets` folder.

The `sample~` folder in the package also includes sample scenes useful for seeing common applications of this library.

Additional information is available in the [Gummi documentation](https://gummi.nicolasmaclean.com)
