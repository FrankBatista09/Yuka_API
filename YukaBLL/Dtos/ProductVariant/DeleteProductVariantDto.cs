﻿using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class DeleteProductVariantDto : DeleteDto
    {
        public int ProductVariantId { get; set; }
    }
}
