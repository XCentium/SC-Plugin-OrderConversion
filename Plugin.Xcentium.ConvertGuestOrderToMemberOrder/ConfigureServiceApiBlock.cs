namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder
{
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System.Threading.Tasks;
    using System.Web.Http.OData.Builder;

    [PipelineDisplayName("ConvertGuestOrderConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");

            var configuration = modelBuilder.Action("ConvertToMemberOrder");

            configuration.Parameter<string>(Constants.Order.OrderId);
            configuration.Parameter<string>(Constants.Order.CustomerId);
            configuration.Parameter<string>(Constants.Order.CustomerEmail);
            configuration.Returns<bool>();

            return Task.FromResult(modelBuilder);

        }
    }
}
