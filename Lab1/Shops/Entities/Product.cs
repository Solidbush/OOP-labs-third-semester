using Shops.Exceptions;

namespace Shops.Entities;

public class Product
{
   private const int MinProductNameLength = 3;
   public Product(string name)
   {
      EnsureProductName(name);
      Name = name;
   }

   public string Name { get; }

   public void EnsureProductName(string productName)
   {
      if (string.IsNullOrWhiteSpace(productName))
      {
         throw new ProductNameValidationExeption("Product's name must not be an empty string or has white spaces");
      }

      productName = productName.Trim();
      if (productName.Length <= MinProductNameLength)
      {
         throw new ProductNameValidationExeption("The product name must be more than three characters long");
      }
   }

   public override bool Equals(object obj)
   {
      if (obj == null || GetType() != obj.GetType())
         return false;
      Product tempShop = (Product)obj;
      return Name == tempShop.Name;
   }

   public override int GetHashCode()
   {
      return HashCode.Combine(Name);
   }
}