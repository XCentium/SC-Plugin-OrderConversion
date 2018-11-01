namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Commands
{
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;
    using Sitecore.Commerce.Plugin.Orders;
    using System.Threading.Tasks;

    public class ConvertGuestOrderToMemberOrderCommand : CommerceCommand
    {
        private readonly CommerceCommander _commerceCommander;

        public ConvertGuestOrderToMemberOrderCommand(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public async Task<Order> Process(CommerceContext commerceContext, ConvertGuestOrderToMemberOrderArgument arg)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                return await _commerceCommander.Pipeline<IConvertGuestOrderToMemberOrderPipeline>().Run(arg, commerceContext.GetPipelineContextOptions());
            }
        }
    }
}
