Sitecore Commerce Plugin to convert a Guest Order into a member Order
======================================

This plugin allows you to convert a guest order into an authenticated member order.  
As you may know, most eCommerce websites today offer the Guest order feature, and after completion give you the option to register and create an account.  
In that scenario, the completed guest order needs to be displayed in the user’s order history.  
Is Sitecore Commerce 9, this means the guest order needs to be transformed to an authenticated order by updating the ‘Contact Component’ and ‘Membership List’.  

Sponsor
=======
This plugin was sponsored and created by Xcentium.


How to Install
==============

1-	Copy the plugin to your Sitecore Commerce Engine Solution and add it as a project.  
2-	Add it as a dependency to your Sitecore.Commerce.Engine project.  

How to Use
==============

After a guest order is submitted, and the account is created, call ‘api/ConvertToMemberOrder()’ endpoint, passing these paremeters:  
  
1-	OrderId: Guest Order id.  
2-	CustomerId: Created Customer id.  
3-	CustomerEmail: Created Customer email.  
  
The order will then be associated with the registered customer and appear in Orders History.


Note:
=====

- If you have any questions, comment or need us to help install, extend or adapt to your needs, do not hesitate to reachout to us at XCentium.
