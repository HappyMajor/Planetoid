<!-- include ../../readme.md#content -->
<!-- #content -->
The most popular and friendly mocking library for .NET

```csharp
  var mock = new Mock<ILoveThisLibrary>();

  // WOW! No record/replay weirdness?! :)
  mock.Setup(library => library.DownloadExists("2.0.0.0"))
      .Returns(true);

  // Use the Object property on the mock to get a reference to the object
  // implementing ILoveThisLibrary, and then exercise it by calling
  // methods on it
  ILoveThisLibrary lovable = mock.Object;
  bool download = lovable.DownloadExists("2.0.0.0");

  // Verify that the given method was indeed called with the expected value at most once
  mock.Verify(library => library.DownloadExists("2.0.0.0"), Times.AtMostOnce());
```

Moq also is the first and only library so far to provide Linq to Mocks, so that the 
same behavior above can be achieved much more succinctly:

```csharp
  ILoveThisLibrary lovable = Mock.Of<ILoveThisLibrary>(l =>
    l.DownloadExists("2.0.0.0") == true);

  // Exercise the instance returned by Mock.Of by calling methods on it...
  bool download = lovable.DownloadExists("2.0.0.0");

  // Simply assert the returned state:
  Assert.True(download);
  
  // If you want to go beyond state testing and want to 
  // verify the mock interaction instead...
  Mock.Get(lovable).Verify(library => library.DownloadExists("2.0.0.0"));
```

You can think of Linq to Mocks as "from the universe of mocks, give me one whose behavior 
matches this expression".

Check out the [Quickstart](https://github.com/moq/moq/wiki/Quickstart) for more examples!

<!-- #content -->
<!-- ../../readme.md#content -->
<!-- include ../../readme.md#sponsors -->
<!-- #sponsors -->

![Sponsors](https://raw.githubusercontent.com/devlooped/sponsors/main/assets/sponsors.svg) Sponsors
============

Special thanks to the following gold sponsors of this project:

[![Supported by Amazon Web Services](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/aws.png "Supported by Amazon Web Services")](https://github.com/aws)
[![Supported by Clarius](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Supported by Clarius")](https://github.com/clarius)

And to all our sponsors!

<!-- include https://github.com/devlooped/sponsors/raw/main/sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![C. Augusto Proiete](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/augustoproiete.png "C. Augusto Proiete")](https://github.com/augustoproiete)
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![SandRock](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sandrock.png "SandRock")](https://github.com/sandrock)
[![Andy Gocke](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agocke.png "Andy Gocke")](https://github.com/agocke)
[![Stephen Shaw](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/decriptor.png "Stephen Shaw")](https://github.com/decriptor)
[![Torutek](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/torutek-gh.png "Torutek")](https://github.com/torutek-gh)
<!-- https://github.com/devlooped/sponsors/raw/main/sponsors.md -->
<!-- ../../readme.md#sponsors -->

<!-- Exclude from auto-expansion by devlooped/actions-include GH action -->
<!-- exclude -->