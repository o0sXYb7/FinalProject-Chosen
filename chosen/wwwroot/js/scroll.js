$(document).ready(function () {
    // 捲軸偵測距離頂部超過 50 才顯示按鈕
    $(window).scroll(function () {
        if ($(window).scrollTop() > 50) {
            if ($(".back-top").hasClass("hide")) {
                $(".back-top").toggleClass("hide");
            }
        } else {
            $(".back-top").addClass("hide");
        }
    });

    // 點擊按鈕回頂部
    $(".back-top").on("click", function (event) {
        $("html, body").animate(
            {
                scrollTop: 0
            },
            500 // 回頂部時間為 500 毫秒
        );
    });
});

var image = document.getElementById("loginimg");

image.addEventListener("mouseover", function () {
    this.src = "img/reduser.png";
});

image.addEventListener("mouseout", function () {
    this.src = "img/user.png";
});