using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib
{
    public class Calculation
    {
        /// <summary>
        /// Расчет материалов
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="materialType"></param>
        /// <param name="count"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int GetQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            if(count <= 0 || width <= 0 || length <= 0)
            {
                return -1;
            }

            float area = width * length;

            double total = 0;

            switch (productType)
            {
                case 1:
                    total = area * count * 1.1;
                    break;
                case 2:
                    total = area * count * 2.5;
                    break;
                case 3:
                    total = area * count * 8.43;
                    break;
                default:
                    return -1;
            }

            switch (materialType)
            {
                case 1:
                    total = total * 0.003 + total;
                    break;
                case 2:
                    total = total * 0.0012 + total;
                    break;
                default:
                    return -1;
            }

            return (int)Math.Ceiling(total);
        }
    }
}
