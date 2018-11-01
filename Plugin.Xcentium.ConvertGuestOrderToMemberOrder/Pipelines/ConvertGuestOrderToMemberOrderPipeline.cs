namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines
{
    using Microsoft.Extensions.Logging;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Orders;
    using Sitecore.Framework.Pipelines;

    public class ConvertGuestOrderToMemberOrderPipeline : CommercePipeline<ConvertGuestOrderToMemberOrderArgument, Order>, IConvertGuestOrderToMemberOrderPipeline
    {
        public ConvertGuestOrderToMemberOrderPipeline(IPipelineConfiguration<IConvertGuestOrderToMemberOrderPipeline> configuration, ILoggerFactory loggerFactory)
            : base(configuration, loggerFactory)
        {

        }
    }
}
