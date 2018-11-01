namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines
{
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Orders;
    using Sitecore.Framework.Pipelines;

    [PipelineDisplayName("Events.pipeline.ConvertGuestOrderToMemberOrderPipeline")]
    public interface IConvertGuestOrderToMemberOrderPipeline : IPipeline<ConvertGuestOrderToMemberOrderArgument, Order, CommercePipelineExecutionContext>
    {

    }
}
