namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder
{
    using Microsoft.Extensions.DependencyInjection;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Blocks;
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Configuration;
    using System.Reflection;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config
                .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>())
                .AddPipeline<IConvertGuestOrderToMemberOrderPipeline, ConvertGuestOrderToMemberOrderPipeline>(configure =>
                {
                    configure.Add<ConvertGuestOrderToMemberOrderBlock>();
                }));

            services.RegisterAllCommands(assembly);
        }
    }
}
