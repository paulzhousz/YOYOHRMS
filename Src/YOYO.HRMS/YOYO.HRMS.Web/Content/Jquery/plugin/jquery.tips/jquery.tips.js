/// <reference path="../jQuery-vsdoc.js" />

//jQuery���������׼ģ��(���󼶱�Ĳ������)
//ʹ�÷�����$("#ids").pluginName({eventname:"mousemove",sedcss:"sed"});

(function($) {
    //���
	$.fn.Tips = function(options) {
        
        var tip=null;
        var container=null;
        
		var defaults = {//�������Լ���Ĭ��ֵ
            elements:$("body"),
            onShow: function(tip){
                $(this).css("visibility", "visible");
            },
            onHide: function(tip){
                $(this).css("visibility", "hidden");
            },
            showDelay: 100,
            hideDelay: 100,
            className: null,
            offsets: {x: 16, y: 16},
            fixed: false 
        };
		var opts = $.extend(defaults,options);
        
         //function codes in here 
        //��ʼ��
        initialize:function(){
            
            var dom=document.createElement("div");
            this.tip=$(dom);

            if (this.opts.className) this.tip.addClass(this.opts.className);
            this.tip.append("<div class=\"tip-top\"></div>");
            this.container =$("<div class=\"top\"></div>");
            this.tip.append(this.container);
            this.tip.append("<div class=\"tip-bottom\"></div>");
            this.tip.css({"position": "absolute", "top": 0, "left": 0, "visibility": "hidden"});
            this.attach(opts.elements);
        };
        
        attach:function(elements){
            
            elements.append(this.tip);
            return this;
        };
        
	};
})(jQuery);

