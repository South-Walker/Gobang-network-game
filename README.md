南行五子棋
===

基于TCP协议的五子棋联机对战平台
---

> 山就在那里

### 逻辑框图

* ![总览](http://xiaoliming96.com/images/gobang/gobang_main.png)  
* ![服务器部分](http://xiaoliming96.com/images/gobang/gobang_server.png)  
* ![客户机部分](http://xiaoliming96.com/images/gobang/gobang_client.png)  


### 待做事项

>* ~~补充逻辑框图~~
>* ~~完成客户端<=>服务器的同步通信~~
>* 完成客户端<=>服务器<=>客户端的同步通信(服务器部分异步或开线程，用虚拟机测试)
>* 完成聊天系统
>* 用同步通信实现“下棋”
>* 完成客户端<=>服务器的异步通信
>* 完成客户端<=>服务器<=>客户端的异步通信
>* 完成游戏的gui界面

### 日志

#### 2017-5-28

>* 项目立项
>* 添加了ReadMe文件
>   * 绘制了逻辑框图
>     * 总体部分
>     * 服务器部分
>     * 客户机部分
>   * 更新了待做事项
>   * 更新了日志

#### 2017-5-30

>* 完成了客户端<=>服务器的同步通信
>   * 基于TcpClient与TcpListener封装了TcpHelperClient与TcpHelperServer类
>* 添加了GobangClientTest项目方便测试使用