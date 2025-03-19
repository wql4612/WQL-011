namespace two;
using System;
using System.Threading;

public class AlarmClockEventArgs : EventArgs
{
    public DateTime ClockTime { get; set; }  // 时钟时间
    public DateTime AlarmTime { get; set; }   // 用户设置的闹铃时间
}

public class AlarmClock
{
    private DateTime _clockTime;
    private DateTime _alarmTime;
    private bool _alarmTriggered;

    public DateTime ClockTime => _clockTime;
    public DateTime AlarmTime => _alarmTime;

    public event EventHandler<AlarmClockEventArgs> Tick;
    public event EventHandler<AlarmClockEventArgs> Alarm;

    public AlarmClock()
    {
        // 初始化时对齐到整秒
        var now = DateTime.Now;
        _clockTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
    }

    public void SetAlarm(DateTime alarmTime)
    {
        // 自动去除毫秒部分
        _alarmTime = alarmTime.AddTicks(-(alarmTime.Ticks % TimeSpan.TicksPerSecond));
        _alarmTriggered = false;
    }

    public void Start()
    {
        new Thread(() =>
        {
            while (true)
            {
                // 每秒推进模拟时间
                _clockTime = _clockTime.AddSeconds(1);

                var args = new AlarmClockEventArgs
                {
                    ClockTime = _clockTime,
                    AlarmTime = _alarmTime
                };

                // 触发滴答事件
                Tick?.Invoke(this, args);

                // 检查闹铃条件
                if (!_alarmTriggered && _clockTime >= _alarmTime)
                {
                    Alarm?.Invoke(this, args);
                    _alarmTriggered = true; // 防止重复触发
                }

                Thread.Sleep(1000);
            }
        }).Start();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var alarmClock = new AlarmClock();

        // 设置自定义闹铃时间（当前时间+10秒）
        alarmClock.SetAlarm(DateTime.Now.AddSeconds(10));

        alarmClock.Tick += (sender, e) =>
        {
            Console.WriteLine($"[当前时间 {e.ClockTime:HH:mm:ss}] 滴答...");
        };

        alarmClock.Alarm += (sender, e) =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[当前时间 {e.ClockTime:HH:mm:ss}] 响铃！");
            Console.ResetColor();
        };

        // 显示初始信息
        Console.WriteLine($"系统启动时间：{DateTime.Now:HH:mm:ss}");
        Console.WriteLine($"模拟时钟起点：{alarmClock.ClockTime:HH:mm:ss}");
        Console.WriteLine($"闹铃设置时间：{alarmClock.AlarmTime:HH:mm:ss}\n");

        alarmClock.Start();
        Console.ReadLine();
    }
}