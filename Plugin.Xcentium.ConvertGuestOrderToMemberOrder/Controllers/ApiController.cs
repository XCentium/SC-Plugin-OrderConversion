namespace Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sitecore.Commerce.Core;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.OData;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Pipelines.Arguments;
    using Plugin.Xcentium.ConvertGuestOrderToMemberOrder.Commands;

    public class ApiController : CommerceController
    {
        public ApiController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpPost]
        [Route("api/ConvertToMemberOrder()")]
        public async Task<IActionResult> ConvertToMemberOrder([FromBody] ODataActionParameters value)
        {
            if (!value.ContainsKey(Constants.Order.OrderId)) return (IActionResult)new BadRequestObjectResult((object)value);
            if (string.IsNullOrEmpty(value[Constants.Order.OrderId].ToString())) return (IActionResult)new BadRequestObjectResult((object)value);
            if (string.IsNullOrEmpty(value[Constants.Order.CustomerId].ToString())) return (IActionResult)new BadRequestObjectResult((object)value);
            if (string.IsNullOrEmpty(value[Constants.Order.CustomerEmail].ToString())) return (IActionResult)new BadRequestObjectResult((object)value);


            var orderId = value[Constants.Order.OrderId].ToString();
            var customerId = value[Constants.Order.CustomerId].ToString();
            var email = value[Constants.Order.CustomerEmail].ToString();

            var arg = new ConvertGuestOrderToMemberOrderArgument
            {
                CustomerId = customerId,
                OrderId = orderId,
                CustomerEmail = email
            };

            var order = await this.Command<ConvertGuestOrderToMemberOrderCommand>().Process(this.CurrentContext, arg);

            return order == null ? new ObjectResult(false) : new ObjectResult(true);
        }
    }
}
