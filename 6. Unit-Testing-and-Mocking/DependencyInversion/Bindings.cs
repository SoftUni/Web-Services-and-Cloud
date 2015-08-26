namespace DependencyInversion
{
    using DirectoryTraversal;
    using Ninject;
    using Printer;
    using RpgGame;
    using WebCrawler;

    public class Bindings
    {
        public static void RegisterBindings(IKernel kernel)
        {
            kernel.Bind<IFormatter>().To<XmlFormatter>();
            kernel.Bind<IDirectoryProvider>().To<DirectoryProvider>();
            kernel.Bind<IHtmlProvider>().To<HtmlProvider>();
            kernel.Bind<IRandomNumberProvider>().To<RandomNumberProvider>();
        }
    }
}
