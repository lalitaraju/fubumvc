using System.IO;
using Bottles;
using Bottles.Diagnostics;
using FubuMVC.Core;
using FubuMVC.Core.Content;
using FubuMVC.Core.Packaging;
using FubuMVC.Tests.Packaging;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Tests.Content
{
    [TestFixture]
    public class PackageFolderActivatorTester : InteractionContext<PackageFolderActivator>
    {
        private StubPackage package1;
        private StubPackage package2;
        private StubPackage package3;
        private IPackageLog theLog;

        protected override void beforeEach()
        {
            package1 = new StubPackage("1");
            package2 = new StubPackage("2");
            package3 = new StubPackage("3");

            package1.RegisterFolder(BottleFiles.WebContentFolder, "folder1");
            package3.RegisterFolder(BottleFiles.WebContentFolder, "folder3");

            theLog = MockFor<IPackageLog>();

            ClassUnderTest.Activate(new IPackageInfo[] { package1, package2, package3}, theLog);
        }

        [Test]
        public void should_have_added_the_image_content_folder_of_the_2_packages_that_do_have_content_folders()
        {
            MockFor<IContentFolderService>().AssertWasCalled(x => x.RegisterDirectory(Path.Combine("folder1", "content")));
            MockFor<IContentFolderService>().AssertWasCalled(x => x.RegisterDirectory(Path.Combine("folder3", "content")));
        }
    }
}