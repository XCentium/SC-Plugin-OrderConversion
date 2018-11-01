namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Blocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Customers;
    using Sitecore.Commerce.Plugin.ManagedLists;
    using Sitecore.Commerce.Plugin.Orders;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;

    public class ConvertGuestOrderToMemberOrderBlock : PipelineBlock<ConvertGuestOrderToMemberOrderArgument, Order, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;
        public ConvertGuestOrderToMemberOrderBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<Order> Run(ConvertGuestOrderToMemberOrderArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull("The arg can not be null");
            Condition.Requires(arg.OrderId).IsNotNull("The order id can not be null");
            Condition.Requires(arg.CustomerId).IsNotNull("The customer id can not be null");
            Condition.Requires(arg.CustomerEmail).IsNotNull("The customer email can not be null");


            var order = await this._commerceCommander.Command<GetOrderCommand>().Process(context.CommerceContext, arg.OrderId);

            if (order == null) { return null; }

            if (!order.HasComponent<ContactComponent>())
            {
                order.Components.Add(new ContactComponent());
            }

            var contactComponent = order.GetComponent<ContactComponent>();

            if (contactComponent.IsRegistered == false)
            {
                contactComponent.CustomerId = arg.CustomerId;
                contactComponent.ShopperId = arg.CustomerId;
                contactComponent.Email = arg.CustomerEmail;
                contactComponent.IsRegistered = true;
            }
            order.SetComponent(contactComponent);

            var policy = context.GetPolicy<KnownOrderListsPolicy>();
            var membershipsComponent = order.GetComponent<ListMembershipsComponent>();
            membershipsComponent.Memberships.Remove(policy.AnonymousOrders);
            membershipsComponent.Memberships.Add(policy.AuthenticatedOrders);
            membershipsComponent.Memberships.Add(string.Format(policy.CustomerOrders, $"{CommerceEntity.IdPrefix<Customer>()}{arg.CustomerId}"));
            order.SetComponent(membershipsComponent);

            await this._commerceCommander.Pipeline<IRemoveListEntitiesPipeline>().Run(new ListEntitiesArgument(new List<string>() { order.Id }, policy.AnonymousOrders), context);
            await this._commerceCommander.Pipeline<IAddListEntitiesPipeline>().Run(new ListEntitiesArgument(new List<string>() { order.Id }, policy.AuthenticatedOrders), context);
            await this._commerceCommander.Pipeline<IAddListEntitiesPipeline>().Run(new ListEntitiesArgument(new List<string>() { order.Id }, string.Format(policy.CustomerOrders, $"{CommerceEntity.IdPrefix<Customer>()}{arg.CustomerId}")), context);

            await this._commerceCommander.Pipeline<IPersistEntityPipeline>().Run(new PersistEntityArgument(order), context);

            return order;
        }
    }
}
