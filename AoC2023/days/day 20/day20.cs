using AoC2023.tools;

namespace AoC2023.days.day_20;

public class Day20: Day
{
    private enum Pulse
    {
        Low, High
    }
    private abstract class Module
    {
        public readonly string name;
        private List<Module> Destinations = new List<Module>();
        public abstract void ReceivePulse(Module origin, Pulse pulse);

        public Module(String name)
        {
            this.name = name;
        }

        public void AddDestination(Module module)
        {
            Destinations.Add(module);
        }

        protected void SendPulse(Pulse pulse)
        {
            foreach (Module destination in Destinations)
            {
                PulseQueue.Enqueue((this, destination, pulse));
            }
        }
    }

    private class FlipFlop : Module
    {
        private bool status = false; //false = off, true = on

        public FlipFlop(string name) : base(name) { }

        public override void ReceivePulse(Module _, Pulse pulse)
        {
            if (pulse.Equals(Pulse.Low))
            {
                status = !status;

                if (status)
                {
                    SendPulse(Pulse.High);
                }
                else
                {
                    SendPulse(Pulse.Low);
                }
            }
        }
    }

    private class Conjunction : Module
    {
        private Dictionary<string, Pulse> OriginStatus = new Dictionary<string, Pulse>();

        public Conjunction(string name) : base(name) { }

        public void AddOrigin(Module module)
        {
            OriginStatus.Add(module.name, Pulse.Low);
        }

        public override void ReceivePulse(Module origin, Pulse pulse)
        {
            OriginStatus[origin.name] = pulse;

            if (OriginStatus.All(kvp => kvp.Value.Equals(Pulse.High)))
            {
                SendPulse(Pulse.Low);
            }
            else
            {
                SendPulse(Pulse.High);
            }
        }
    }

    private class Broadcaster : Module
    {
        public Broadcaster(string name) : base(name) { }
        public override void ReceivePulse(Module _, Pulse pulse){
            SendPulse(pulse);
        }
    }

    private class Button : Module
    {
        public Button(string name) : base(name) { }
        public override void ReceivePulse(Module _, Pulse __) { }

        public void Press()
        {
            SendPulse(Pulse.Low);
        }
    }

    private class ReceiveOnly : Module
    {
        public ReceiveOnly(string name) : base(name) { }

        public override void ReceivePulse(Module _, Pulse __) { }
    }

    private static Queue<(Module origin, Module destination, Pulse)> PulseQueue = new Queue<(Module, Module, Pulse)>();

    public Day20()
    {
        this.Directory = "day 20";
    }
    public override void Run()
    {
        Dictionary<string, Module> Modules = new Dictionary<string, Module>();
        List<(string, string)> Destinations = new List<(string, string)>();
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            string[] input = line.Split(" -> ");
            Module newModule;
            switch (input[0][0])
            {
                case 'b':
                    newModule = new Broadcaster(input[0]);
                    break;
                case '%':
                    newModule = new FlipFlop(input[0][1..]);
                    break;
                case '&':
                    newModule = new Conjunction(input[0][1..]);
                    break;
                default:
                    newModule = null;
                    break;
            }
            Modules.Add(newModule.name, newModule);
            Destinations.Add((newModule.name, input[1]));
        }

        foreach ((string name, string dests) vals in Destinations)
        {
            Module module;
            Modules.TryGetValue(vals.name, out module);
            foreach (string dest in vals.dests.Split(",", StringSplitOptions.TrimEntries))
            {
                Module destModule;
                if (Modules.TryGetValue(dest, out destModule))
                {
                    module.AddDestination(destModule);
                    if (destModule.GetType() == typeof(Conjunction))
                    {
                        Conjunction destConjunction = (Conjunction)destModule;
                        destConjunction.AddOrigin(module);
                    }
                }
                else
                {
                    module.AddDestination(new ReceiveOnly(dest));
                }
            }
        }

        Button button = new Button("button");
        button.AddDestination(Modules["broadcaster"]);

        long highpulses = 0;
        long lowpulses = 0;

        for (int i = 0; i < 1000; i++)
        {
            button.Press();

            while (PulseQueue.Count > 0)
            {
                (Module origin, Module destination, Pulse pulse) = PulseQueue.Dequeue();
                destination.ReceivePulse(origin, pulse);
                if (pulse == Pulse.High)
                {
                    highpulses++;
                }
                else
                {
                    lowpulses++;
                }
            }
        }

    //    Console.WriteLine(highpulses);
    //    Console.WriteLine(lowpulses);
        Console.WriteLine(highpulses * lowpulses);
    }
}