/*
scaling     是否等比例自动缩放
width       图片最大高
height      图片最大宽
strLoad     加载时要显示的文字或者图片
Method      调整完大小之后执行的方法
*/
jQuery.fn.LoadImage=function(scaling,width,height,strLoad,Method,errorMethod){
    String(strLoad)==""||String(strLoad)=="undefined"?strLoad="加载中...":strLoad;
    return this.each(function(){
        var imgD=$(this);
        var src=$(this).attr("src");
        var img=new Image();
        img.src=src;
        //自动缩放图片
        var AutoScaling=function(){
            if(scaling){
            
                if(img.width>0 && img.height>0){ 
                    if(img.width/img.height>=width/height){ 
                        if(img.width>width){ 
                            imgD.width(width); 
                            imgD.height((img.height*width)/img.width); 
                        }else{ 
                            imgD.width(img.width); 
                            imgD.height(img.height); 
                        } 
                    } 
                    else{ 
                        if(img.height>height){ 
                            imgD.height(height); 
                            imgD.width((img.width*height)/img.height); 
                        }else{ 
                            imgD.width(img.width); 
                            imgD.height(img.height); 
                        } 
                    } 
                } 
            }    
        }
        //火狐浏览器处理
        if(img.complete){
            AutoScaling();
            if(Method)
                Method();
            return;
        }
        $(this).attr("src","");
        var loading=$("<div>"+strLoad+"</div>");  
        imgD.hide();
        imgD.after(loading);
        $(img).load(function(){
            AutoScaling();
            loading.remove();
            imgD.attr("src",this.src);
            imgD.show();
            if(Method)
                Method();
        });
        $(img).error(function(){
            loading.remove();
            imgD.show();
            if(errorMethod){
                errorMethod();
            }
        })
        
    });
}