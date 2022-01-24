using Autofac;
using Autofac.Core;
using Contracts;
using Parsers.HtmlProviding;
using Persistence.Contracts;
using Persistence.Control;
using System;
using System.Collections.Generic;

namespace DataProvider.DependencyResolving
{
    public class DependencyResolver
    {
        public DependencyResolver(Dictionary<string, string> sources)
        {
            _builder = new ContainerBuilder();

            #region Creating dependencies in storage
            _builder.RegisterType<ArticleStorager>().As<IArticleStorager>();
            #endregion

            #region Creating dependencies in parser
            _builder.RegisterType<HtmlProviderDeploy>().As<IHtmlProvider>();

            var currentName = "codeproject";
            //InjectParser<CodeprojectParser.ArticlePageParser, CodeprojectParser.PageParser>(currentName, sources);

            currentName = "stackoverflow";
            InjectParser<StackOverflowParser.ArticlePageParser, StackOverflowParser.PageParser>(currentName, sources);

            currentName = "medium";
            InjectParser<MediumParser.ArticlePageParser, MediumParser.PageParser>(currentName, sources);
            #endregion

            _container = _builder.Build();
        }

        public ParsersFactory BuildParsers()
        {
            var parsers = _container.Resolve<IEnumerable<PageParserBase>>();
            var factory = new ParsersFactory(parsers);

            return factory;
        }

        public IArticleStorager BuildStorager()
        {
            var storager = _container.Resolve<IArticleStorager>();

            return storager;
        }

        private void InjectParser<TArticlePageParser, TPageParser>(string currentName, Dictionary<string, string> sources)
            where TArticlePageParser : IArticlePageParser
        {
            _builder.RegisterType<TArticlePageParser>()
                .As<IArticlePageParser>();
            _builder.RegisterType<TPageParser>()
                .As<PageParserBase>()
                .WithParameters(new List<Parameter>() {
                    new NamedParameter(_articleArg, (TArticlePageParser)Activator.CreateInstance(
                        typeof(TArticlePageParser), new object[] { new HtmlProviderDeploy() })
                        ),
                    new NamedParameter(_urlArg, sources[currentName]),
                    new NamedParameter(_sourceArg, currentName),
                    new NamedParameter(_storagerArg, new ArticleStorager())
                });
        }

        private ContainerBuilder _builder;
        private IContainer _container;

        private const string _articleArg = "articleParser";
        private const string _urlArg = "url";
        private const string _sourceArg = "sourceName";
        private const string _storagerArg = "storager";
    }
}
