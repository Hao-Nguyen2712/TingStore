$(document).ready(function () {

    loadCart();

    $(document).ready(function () {
        loadCart();

        $(".add-to-cart-btn").click(function (event) {
            event.preventDefault();

            var productId = $(this).data("product-id");
            var productName = $(this).data("product-name");
            var price = parseFloat($(this).data("price"));
            var productImage = $(this).data("product-image");

            if (isNaN(price)) {
                showErrorAlert("Invalid price value!");
                return;
            }

            var formData = {
                ProductId: productId,
                ProductName: productName,
                Price: price,
                ProductImage: productImage,
                Quantity: 1
            };

            var cartRequest = {
                Id: 1, // Lấy ID user từ session hoặc cookie nếu cần
                Items: [formData]
            };

            console.log("Cart Request:", JSON.stringify(cartRequest));

            $.ajax({
                url: "http://localhost:5001/apigateway/Cart/CreateCart",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(cartRequest),
                success: function (response) {
                    if (response.success) {
                        showSuccessAlert("Add to Cart Successful");
                        setTimeout(loadCart, 500);
                    } else {
                        showErrorAlert(response.message || "Add to Cart Failed");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("Error when adding to cart:", xhr);
                    console.log("Status:", status);
                    console.log("Error:", error);
                    console.log("Response Text:", xhr.responseText);

                    let errorMessage = "Error when adding to cart!";
                    if (xhr.responseJSON && xhr.responseJSON.message) {
                        errorMessage = xhr.responseJSON.message;
                    }
                    showErrorAlert(errorMessage);
                }
            });
        });
    });


    $(".product-list").on("click", ".increment, .decrement", function () {
        let inputField = $(this).siblings(".number");
        let currentQuantity = parseInt(inputField.val());
        let isIncrement = $(this).hasClass("increment");
        let newQuantity = isIncrement ? currentQuantity + 1 : currentQuantity - 1;

      
        if (newQuantity < 1) {
            return;
        }

        inputField.val(newQuantity);

        let productId = $(this).closest(".product-item").find(".cancel").data("id");
        let userId = 1; 

        
        updateItemQuantity(userId, productId, newQuantity);
    });

    $(".product-list").on("click", ".cancel", function () {
        let productId = $(this).data("id");
        let userId = 1; 

        deleteProductFromCart(userId, productId);
    });

    $("#checkout-btn").click(function () {
        let userId = 1; 
        let productIds = [];

     
        $(".product-item").each(function () {
            let productId = $(this).find(".cancel").data("id");
            if (productId) {
                productIds.push(productId);
            }
        });

        // Kiểm tra nếu giỏ hàng rỗng
        if (productIds.length === 0) {
            alert("Giỏ hàng của bạn đang trống!");
            return;
        }

       
        $.ajax({
            url: "http://localhost:5001/apigateway/cart/Checkout",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                UserId: userId,
                ProductIds: productIds
            }),
            success: function (response) {
                console.log("Thanh toán thành công", response);
                showSuccessAlert("Đơn hàng của bạn đã được xử lý!", "Thanh toán thành công!");

                
                $(".product-list").empty();
                $(".price-total span:last").text("Tổng cộng: 0 VND");
                $(".cart-button h6").text("0 VND");
                $("h6:contains('Shopping Cart')").text("Shopping Cart (0)");

               
                setTimeout(() => {
                    window.location.href = "http://localhost:5106/User/Order";
                }, 3000);
            },
            error: function (xhr) {
                console.log("Lỗi khi thanh toán:", xhr);

                let errorMessage = "Có lỗi xảy ra khi xử lý thanh toán!";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                }

                // Hiển thị thông báo lỗi bằng SweetAlert
                showErrorAlert(errorMessage);
            }
        });
    });

});

function loadCart(userId) {
    function loadCart(userId) {
        $.ajax({
            url: `http://localhost:5001/apigateway/Cart?userId=${userId}`, // Truyền userId qua query
            type: "GET",
            dataType: "json",
            success: function (response) {
                renderCart(response);
            },
            error: function () {
                console.log("Lỗi khi gọi API giỏ hàng!");
            }
        });
    }

    function renderCart(response) {
        let productList = $(".product-list");
        productList.empty();

        let totalPrice = 0;
        let totalItems = 0;

        response.items.forEach(item => {
            let productHtml = `
            <li class="product-item mb-24">
                <span class="item-image">
                    <img src="${item.productImage}" alt="Product Photo">
                </span>
                <div class="product-text">
                    <div class="prod-title mb-16">
                        <h6>${item.productName}</h6>
                        <a href="javascript:;" class="cancel" data-id="${item.productId}">
                            <img src="/user/assets/media/images/cancel.png" alt="">
                        </a>
                    </div>
                    <div class="prod-desc">
                        <div>
                            <p class="fw-500 mb-8">Số lượng: <span class="quantity-text">${item.quantity}</span></p>
                            <p class="fw-500">Giá: ${formatCurrencyVND(item.price || 0)}</p>
                        </div>
                        <div class="quantity quantity-wrap d-inline-flex">
                            <div class="input-area quantity-wrap">
                                <input class="decrement" type="button" value="-">
                                <input type="text" name="quantity" value="${item.quantity}" maxlength="2" size="1" class="number">
                                <input class="increment" type="button" value="+">
                            </div>
                        </div>
                    </div>  
                </div>
            </li>
            <li class="hr-line mb-24"></li>
        `;
            productList.append(productHtml);
            totalPrice += item.price * item.quantity;
            totalItems += item.quantity;
        });

        $(".price-total span:last").text(`Tổng cộng: ${formatCurrencyVND(totalPrice)}`);
        $(".cart-button h6").text(`${formatCurrencyVND(totalPrice)}`);
        $("h6:contains('Shopping Cart')").text(`Shopping Cart (${totalItems})`);
    }


}

function updateItemQuantity(userId, productId, quantity) {
    $.ajax({
        url: "http://localhost:5001/apigateway/cart/UpdateItemQuantity",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            UserId: userId,
            ProductId: productId,
            Quantity: quantity
        }),
        success: function () {
            console.log("Cập nhật số lượng thành công");
            loadCart(); // Reload giỏ hàng
        },
        error: function () {
            console.log("Lỗi khi cập nhật số lượng sản phẩm!");
        }
    });
}


function deleteProductFromCart(userId, productId) {
    Swal.fire({
        title: "Are you sure?",
        text: "Do you want to remove this product from your cart?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "Cancel"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "http://localhost:5001/apigateway/cart/DeleteProductFromCartCommand",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    UserId: userId,
                    ProductIdToRemove: [String(productId)]
                }),
                success: function () {
                    Swal.fire({
                        icon: "success",
                        title: "Deleted!",
                        text: "The product has been removed from your cart.",
                        timer: 1500,
                        showConfirmButton: false
                    }).then(() => {
                        location.reload(); // Reload the entire page
                    });

                    loadCart(); // Refresh the cart modal
                },
                error: function () {
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "Failed to remove the product. Please try again!"
                    });
                }
            });
        }
    });
}


function formatCurrencyVND(amount) {
    return amount.toLocaleString("vi-VN", { style: "currency", currency: "VND" });
}
