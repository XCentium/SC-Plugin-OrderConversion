namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments
{
    using Sitecore.Commerce.Core;

    public class ConvertGuestOrderToMemberOrderArgument : PipelineArgument
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
    }
}
