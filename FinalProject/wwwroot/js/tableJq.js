$(document).ready(function () {

        //利用cookie儲存上一次每頁顯示筆數
        let savedPageLength = getCookie('datatablePageLength');
        let defaultPageLength = 10; //初始10筆
        if (savedPageLength) {
            defaultPageLength = parseInt(savedPageLength);
        }

        // Setup - add a text input to each footer cell
        $('#mainTable tfoot th').each(function () {
            var title = $(this).text();
            if (title == "")
            {
                return
            }
            $(this).html('<input type="text" placeholder="搜尋 ' + title + '" />');
        });

        // DataTable
        var table = $('#mainTable').DataTable({
            'lengthMenu': [[10, 25, 50, -1], [10, 25, 50, "All"]],
            'pageLength': defaultPageLength,
            "autoWidth": false,
            language: { url: "//cdn.datatables.net/plug-ins/1.13.4/i18n/zh-HANT.json" },//中文化圖表中的小項目
            "drawCallback": function (settings) {
                setCookie('datatablePageLength', settings._iDisplayLength, 365);
            },
            initComplete: function () {
                // Apply the search
                this.api()
                    .columns()
                    .every(function () {
                        var that = this;

                        $('input', this.footer()).on('keyup', function () {
                            if (that.search() !== this.value) {
                                that.search(this.value).draw();
                            }
                        });
                    });
            },
        });

        // 取得 Cookie
        function getCookie(name) {
            let cookieValue = null;
            if (document.cookie && document.cookie !== '') {
                let cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    let cookie = cookies[i].trim();
                    if (cookie.substring(0, name.length + 1) === (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break;
                    }
                }
            }
            return cookieValue;
        }

        // 設定 Cookie
        function setCookie(name, value, days) {
            let expires = '';
            if (days) {
                let date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = '; expires=' + date.toUTCString(); //存入cookie中的過期時間
            }
            document.cookie = name + '=' + encodeURIComponent(value) + expires + '; path=/';
        }


});
