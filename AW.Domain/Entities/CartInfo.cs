namespace AW.Domain.Entities;

public partial class Cart
{
    public decimal? Total
    {
        get
        {
            decimal? totalPrice = 0;

            // Calculamos el total de las artesanias
            foreach (var item in Craft)
            {
                totalPrice += item.Price;
            }

            return totalPrice; // Devolvemos el total
        }
    }
}
