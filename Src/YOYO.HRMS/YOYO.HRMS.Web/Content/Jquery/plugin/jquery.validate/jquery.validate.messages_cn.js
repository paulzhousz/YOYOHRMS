/*
* Translated default messages for the jQuery validation plugin.
*/
//手机验证
jQuery.validator.addMethod("isTelphone", function (value, element) {
    var tel = /^(130|131|132|133|134|135|136|137|138|139|150|153|157|158|159|180|187|188|189)\d{8}$/;
    return tel.test(value) || this.optional(element);
}, "请输入正确的手机号码");

//字符集
jQuery.validator.addMethod("isUserName", function (value, element) {
    return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);
}, "用户名只能包括中文字、英文字母、数字和下划线");

//中文字两个字节       
jQuery.validator.addMethod("byteRangeLength", function (value, element, param) {
    var length = value.length;
    for (var i = 0; i < value.length; i++) {
        if (value.charCodeAt(i) > 127) {
            length++;
        }
    }
    return this.optional(element) || (length >= param[0] && length <= param[1]);
}, "请确保输入的值在3-15个字节之间(一个中文字算2个字节)");

//身份证号码验证       
jQuery.validator.addMethod("isIdCardNo", function (value, element) {
    return this.optional(element) || isIdCardNo(value);
}, "请正确输入您的身份证号码");

//手机号码验证       
jQuery.validator.addMethod("isMobile", function (value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "请正确填写您的手机号码");

//电话号码验证       
jQuery.validator.addMethod("isTel", function (value, element) {
    var tel = /^\d{3,4}-?\d{7,9}$/;    //电话号码格式010-12345678   
    return this.optional(element) || (tel.test(value));
}, "请正确填写您的电话号码");

//联系电话(手机/电话皆可)验证   
jQuery.validator.addMethod("isPhone", function (value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var tel = /^\d{3,4}-?\d{7,9}$/;
    return this.optional(element) || (tel.test(value) || mobile.test(value));

}, "请正确填写您的联系电话");

//邮政编码验证       
jQuery.validator.addMethod("isZipCode", function (value, element) {
    var tel = /^[0-9]{6}$/;
    return this.optional(element) || (tel.test(value));
}, "请正确填写您的邮政编码");




//异步验证用户名
jQuery.validator.addMethod("AjaxUserName", function (value, element) {
    var result = false;
    // 设置同步
    $.ajaxSetup({
        async: false
    });
    var param = {
        ChkNameValue: value
    };
    $.post("/Ajax/AjaxUserName.aspx", param, function (data) {
        result = (true == data);
    });
    // 恢复异步
    $.ajaxSetup({
        async: true
    });
    return result;
}, "该用户名已经存在！");

//异步验证手机号码
jQuery.validator.addMethod("AjaxUserMobile", function (value, element) {
    var result = false;
    // 设置同步
    $.ajaxSetup({
        async: false
    });
    var param = {
        ChkNameValue: value
    };
    $.post("/Ajax/AjaxUserMobile.aspx", param, function (data) {
        result = (true == data);
    });
    // 恢复异步
    $.ajaxSetup({
        async: true
    });
    return result;
}, "该手机号码已经存在！");


//异步验证昵称
jQuery.validator.addMethod("AjaxUserNickname", function (value, element) {
    var result = false;
    // 设置同步
    $.ajaxSetup({
        async: false
    });
    var param = {
        ChkNameValue: value
    };
    $.post("/Ajax/AjaxUserNickname.aspx", param, function (data) {
        result = (true == data);

    });
    // 恢复异步
    $.ajaxSetup({
        async: true
    });
    return result;
}, "该昵称已经存在！");

//异步验证验证码
jQuery.validator.addMethod("AjaxCheckCode", function (value, element) {
    var result = false;
    // 设置同步
    $.ajaxSetup({
        async: false
    });
    var param = {
        ChkNameValue: value
    };
    $.post("/Ajax/AjaxCheckCode.aspx", param, function (data) {
        result = (true == data);

    });
    // 恢复异步
    $.ajaxSetup({
        async: true
    });
    return result;
}, "验证码不正确");

jQuery.extend(jQuery.validator.messages, {
    required: "不能为空",
    remote: "请修正该字段",
    email: "请输入正确格式的电子邮件",
    url: "请输入合法的网址",
    date: "请输入合法的日期",
    dateISO: "请输入合法的日期 (ISO).",
    number: "请输入合法的数字",
    digits: "只能输入整数",
    creditcard: "请输入合法的信用卡号",
    equalTo: "请再次输入相同的值",
    accept: "请输入拥有合法后缀名的字符串",
    maxlength: jQuery.validator.format("请输入一个长度最多是 {0} 的字符串"),
    minlength: jQuery.validator.format("请输入一个长度最少是 {0} 的字符串"),
    rangelength: jQuery.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
    range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
    max: jQuery.validator.format("请输入一个最大为 {0} 的值"),
    min: jQuery.validator.format("请输入一个最小为 {0} 的值")
});












//表单验证
$(document).ready(function () {
    $(".input,.login_input,.textarea").focus(function () {
        $(this).addClass("focus");
    }).blur(function () {
        $(this).removeClass("focus");
    });

    //输入框提示,获取拥有HintTitle,HintInfo属性的对象
    $("[HintTitle],[HintInfo]").focus(function (event) {
        $("*").stop(); //停止所有正在运行的动画
        $("#HintMsg").remove(); //先清除，防止重复出错
        var HintHtml = "<ul id=\"HintMsg\"><li class=\"HintTop\"></li><li class=\"HintInfo\"><b>" + $(this).attr("HintTitle") + "</b>" + $(this).attr("HintInfo") + "</li><li class=\"HintFooter\"></li></ul>"; //设置显示的内容
        var offset = $(this).offset(); //取得事件对象的位置
        $("body").append(HintHtml); //添加节点
        $("#HintMsg").fadeTo(0, 0.85); //对象的透明度
        var HintHeight = $("#HintMsg").height(); //取得容器高度
        $("#HintMsg").css({ "top": offset.top - HintHeight + "px", "left": offset.left + "px" }).fadeIn(500);
    }).blur(function (event) {
        $("#HintMsg").remove(); //删除UL
    });


    
});


function ReadyValidate(id) {
    $("#" + id).validate({
        //手动设置错误信息的显示方式
        errorPlacement: function (error, element) {
            error.appendTo(element.parent().next());
            if (element.is(":radio"))
                error.appendTo(element.siblings("span"));
            else if (element.is(":checkbox")) {
                error.appendTo(element.siblings("span"));
            }
            else
                error.appendTo(element.parent());
        },
        //出错时添加的标签
        errorElement: "span",
        //当未通过验证的元素获得焦点时,并移除错误提示
        //focusCleanup: true,
        //验证通过后执行的动作
        success: function (label) {
            //正确时的样式
            label.parent().find('.span-error').show(); label.html("").addClass("field-validation-valid");
        }
    });
}

//验证表单是否验证通过
function CheckForm() {
    if ($("#EditForm").valid()) { top.window.DivDialog.Operating(); }
}

//验证表单是否验证通过
function CheckNewForm() {
    if ($("#EditForm").valid()) {$('#ID').val(0);top.window.DivDialog.Operating();}
}