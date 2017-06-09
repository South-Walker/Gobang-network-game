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
>* ~~服务器开线程负责监听多个客户端的连接请求~~
>* ~~完成客户端<=>服务器<=>客户端的同步通信~~
>* ~~完成聊天系统~~
>* ~~完成游戏的gui界面~~
>* ~~接住客户机断开时抛出的异常~~
>* ~~补充状态码说明~~
>* ~~由服务端分配执黑执白~~
>* ~~用特定编码表示“下棋”~~
>* ~~用同步通信实现“下棋”~~
>* ~~添加下棋规则~~
>* 把类图完成
>* 客户端在落子前要判断是否已有子
>* 棋局结束后，初始化应用并让双方回到等待队列
>* 改成用List存储Game与Player
>* 开线程监听Player与Game的终止，从对应的List中移除实例
>* 修改成异步形式
>* 完善系统提示
>* 添加下棋AI
>* 用Xamarin for VS移植到安卓平台
>* 用Xamarin for VS移植到IOS平台

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

#### 2017-5-31

>* 完成了客户端<=>服务器<=>客户端的同步多线程通信
>* 重构代码
>   * 重新封装了TcpHelperServer类，令Player类承担了其通信部分的职能
>   * 封装了Player与Game类，在TcpHelperServer类中用队列存储了所有Player与Game的集合
>* 服务器现在分配了多个线程专门执行特定任务
>   * TcpHelperServer类的ListenerThread，GameMakerThread分别负责生成TcpClient并封装成Player，将Player两两分组
>   * Player类的ReaderThread负责监听网络流，读到信息后存入MessageBox
>   * Game类的TalkerThread负责监听MessageBox，将比赛双方的信息分别写入对方的网络流
>* 习惯代替配置（命名规范）
>   * 线程的命名在末尾加上Thread，托管给线程的方法在末尾加上Threadwork，所有线程属于实例变量，在实例化的最后被赋值与启用

#### 2017-6-1

>* 现在当客户端断开连接时，服务端不会崩溃
>   * 作为处理，客户端对应的Player实例中将有一个属性标识该连接已经断开
>   * 如果断开的客户端所对应的Player实例属于一个Game实例，那么Game实例将会被标记为终止，Game中的另一位Player将回到等待队列
>   * 上述所有情况发生时，对应的线程会被Abort();

#### 2017-6-2

>* 服务端
>   * 新建了Counter结构，包含TotalPlayer与TotalGame两个静态公共变量，用于记录当前在线人数与对局数，作用于等同于全局变量
>   * 在建立，销毁Player，Game实例时对应的，会维护Counter结构
>* 客户端
>   * 完善了聊天系统
>   * 封装了ControlHander类用来实例化委托与跨线程调用控件
>   * 实例化委托时用到了Lambda表达式，很开心
>```c#
>setroom = message => rtxtRoom.AppendText("\r\n" + message);
>setstate = message => rtxtState.AppendText("\r\n" + message);
>```
>   * 重构了代码
>       * 增加了LoginForm窗体
>       * 把封装好的类放到了单独的文件中
>       * 用如下代码保证了TcpHelperServer类只有唯一的一个实例
>```c#
>public class TcpHelperClient
>{
>   public static TcpHelperClient main = new TcpHelperClient();
>   private TcpHelperClient()
>   {
>       //感谢设计模式之禅，真的是好书
>   }
>}
>```

#### 2017-6-3

>* 服务端
>   * 现在，服务器对客户端发送的所有信息都加上了前缀，状态码为"$$",聊天内容为"!"
>* 客户端
>   * 完成了下棋的图形化界面，由Printer类封装
>   * 简单的把Printer类与对应的控件复制到了客户端程序，并添加了is_my_time属性标识是否为当前回合
>   * 优化了用户体验
>* 对状态码的意义在README文件中进行了说明

#### 2017-6-4

>* 在GobangGame中修改了最小化后绘图消失的bug，等待移植到客户端中

#### 2017-6-5

>* 服务端
>   * 用CodeNum类以常量的形式封装了状态码
>* 客户端
>   * 移植了Printer类
>   * 客户端间的行子信息现在可以通过服务端传递，并用另一客户端绘制
>   * 客户端现在能区分与处理来自服务端的状态码
>* 初步完成了整个五子棋的程序，但是写的这坨屎还要花时间重构（截止目前，2017-6-5第二次push，不包括所有测试代码，代码量是669行）

#### 2017-6-7

>* 题外话：
>   * 昨天配了一天环境，终于把Xamarin for VS配好了，现在可以进行安卓平台的应用开发了（VS2015开发ios应用需要macos和xcode，这个等暑假升级成2017吧）
>   * 现在其实要做的是要重构好代码，补充逻辑框图，不过考虑到心情，还是先完成五子棋的规则
>   * 要做的事情大致是，完全实现在线对战=》重构代码，明确设计模式=》使用set，reset，waitone方法修改对线程同步的实现
>   * 规则的实现：
>       * 棋谱被存储在GameManual类中，以二维数组形式实现，同时该类也封装了判断胜负的算法（仅以5子相连为判断依据）
>   * KeepCoding：你每天刷题，做项目，写日志，去了解那些抽象的设计模式，去了解那些约束自己的编码原则，花时间去配这些莫名其妙的环境，
不是为了用来与人争执的，不是为了与人闲扯的，你负责实现就好，KeepCoding！Talk is cheap，show them your code
>* 写完了判断胜负的算法，主要的内存开销在于一个15*15的int二维数组，一个int是32位4字节，那么存储一个棋盘是900B，1W人同时对战也就需要4.5MB是可以放在服务器端的，放在服务器端也更安全
>* 服务端
>   * 现在，服务器端会用GameManual类来存储棋谱，在Game类中存储了该类的一个实例
>* 客户端
>   * 现在，生成的205状态码会发送具体的棋盘坐标
>* 新增了206与207状态码，标识胜负，具体可见附录
>* 今天应该说是阶段性的完成任务了，真正的实现了可以和别人联网对弈，但是，要注意的是，如果明天我不能好好的重构一下现在这坨屎，那这800来行代码我一定是不会再看的

#### 2017-6-8
>* 重构了服务端代码
>   * 把CodeNum写在了公共类里面，并在客户端与服务端中都添加了引用
>   * 重写了Game类
>   * 重写了Player类

### 附录

#### 状态码及其意义与说明

* 除205以外的状态码只会由服务器发送
* 状态码被封装在CodeNum类
* 状态码为一个三位数，为与其他信息区别，在前方有"$"作前缀
* 状态码的百位标识状态，个位与十位标识具体种类
* 2**：标识通信成功，程序按既定的逻辑运行
    * 200：当用户与服务器成功建立TCP连接时，服务器会向用户发送200标识码
    * 201：当在等待队列的TCP连接数大于2个时，队列前两个出队，组成对局，同时服务器向双方发送201标识码
    * 202：当组成对局时，服务器向红方发送202标识码，表示执白
    * 203：当组成对局时，服务器向黑方发送202标识码，表示执黑
    * 204：当轮到用户行棋时，服务器向对应用户发送204标识码，一般的，会伴随着205标识码的发送
    * 205：一般以$205:aa,bb的形式发送，aa，bb表示对方行子的鼠标坐标//在2017-6-7的更新中改为了发送具体的棋盘坐标
    * 206：当行子后服务器判断分出胜负了，向胜方发送206状态码
    * 207：当行子后服务器判断分出胜负了，向负方发送207状态码
* 4**：标识通信出现问题，程序将回到上一步逻辑
    * 404：当在已建立的对局中，有一方的TCP连接中断，那么便会向他的对手发送404标识码
* 1**：标识服务器的全频广播
    * 199：服务器向连接状态下的所有用户发送全频广播默认使用的标识码，不包含任何信息