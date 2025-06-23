using FluentValidation;
using RestaurantReservation.API.Models.OrderItems;

namespace RestaurantReservation.API.Validators.OrderItems
{
    public class OrderItemUpdateValidator : AbstractValidator<OrderItemUpdatedDto>
    {
        public OrderItemUpdateValidator()
        {
        
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }


    }
}

