using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class CustomersModel
    {
        public int CustomerId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must contain at least 3 characters")]
        [MaxLength(40, ErrorMessage = "More than 40 character is not allow")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 40 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).+$", ErrorMessage = "Password must contain at least one lowercase letter and one uppercase letter.")]
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? IsAdmin { get; set; }

        // Navigation properties
        public ICollection<OrdersModel> Orders { get; set; }
        public ICollection<ShoppingCartModel> ShoppingCart { get; set; }
        public ICollection<WishlistModel> Wishlist { get; set; }

    }
}