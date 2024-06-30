﻿function activeAddress(addressId) {
    Swal.fire({
        title: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "info",
        confirmButtonText: "بله ، مطمعئن هستم",
        cancelButtonText: "خیر",
        showCancelButton: true,
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/profile/addresses/SetActiveAddress?addressId=" + addressId,
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
            }).done(function (data) {
                var res = JSON.parse(data);
                if (res.Status === 200) {
                    Success("", res.Message, true);
                } else {
                    ErrorAlert("", res.Message, res.IsReload);
                }
            })
        }
    });
}
function addToCart(inventoryId, count) {
    var token = $("#ajax-token input[name='__RequestVerificationToken']").val();

    $.ajax({
        url: `/shopcart/addItem?inventoryId=${inventoryId}&count=${count}`,
        type: "post",
        data: {
            __RequestVerificationToken: token
        },
        beforeSend: function (xhr) {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
    }).done(function (data) {
        var res = JSON.parse(data);
        CallBackHandler(res);
    });
}

//$(document).ready(function () {
//    $.ajax
//        ({

//            url: "/shopCart/shopCartDetail",
//            type: "get"
//        }).done(function (data)
//        {
//            try {
//                var bagElements = $(".bag-items-number");
//                console.log(bagElements)
//                bagElements[0].innerHTML = data.count;
//                bagElements[1].innerHTML = data.count;
//            }
//            catch {

//            }
//            $(".cart-footer .total").html(data.price)
//            data.items.map(i) =>
//            {
//                $(".cart-items ul")
//                    .append(`<li class="cart-item">
//                                            <span class="d-flex align-items-center mb-2">
//                                                <a href="#">
//                                                  <img src="https://localhost:44343//images/products/${i.productImageName}">
//                                                </a>
//                                                <span>
//                                                    <a href="#">
//                                                        <span class="title-item">
//                                                            ${i.productTitle}
//                                                        </span>
//                                                    </a>
//                                                    <span class="color d-flex align-items-center">
//                                                        تعداد:
//                                                        <span">${i.count}</span>
//                                                    </span>
//                                                </span>
//                                            </span>
//                                            <span class="price">4,050,000 تومان</span>
//                                            <button class="remove-item">
//                                                <i class="far fa-trash-alt"></i>
//                                            </button>
//                                        </li>`

//            });
//        });
//});


$(document).ready(function () {
    $.ajax({
        url: "/shopcart/shopcartDetail",
        type: "get"
    }).done(function (data) {
        var bagElements = $(".bag-items-number");
        try {
            bagElements[0].innerHTML = data.count;
            bagElements[1].innerHTML = data.count;
        } catch {

        }
        $(".cart-footer .total").html(data.price)
        if (data.items.length == 0) {
            $('.cart-list ul').remove();
        } else {
            data.items.map((i) => {
                $(".cart-items .do-nice-scroll").append(`
                        <li class="cart-item">
                                    <span class="d-flex align-items-center mb-2">
                                        <a href="/product/${i.productSlug}">
                                            <img src="https://localhost:44343/images/products/${i.productImageName}" alt="">
                                        </a>
                                        <span>
                                            <a href="#">
                                                <span class="title-item">
                                                    ${i.productTitle}
                                                </span>
                                            </a>
                                            <span class="color d-flex align-items-center">
                                                تعداد:
                                                <label style='display:contents'>${i.count}</label>
                                            </span>
                                        </span>
                                    </span>
                                    <span class="price">${splitNumber(i.totalPrice)} تومان</span>
                                    <button class="remove-item" onclick="DeleteItem('/ShopCart/DeleteItem?id=${i.id}','')">
                                        <i class="far fa-trash-alt"></i>
                                    </button>
                                </li>
            `);
            })
        }

    });
});

function splitNumber(value) {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}