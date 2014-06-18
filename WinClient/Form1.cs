using MyServiceContract;
using Castle.Facilities.WcfIntegration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinClient
{
    public partial class Form1 : Form
    {
        private readonly IHelloService _helloService;

        public Form1(IHelloService helloService)
        {
            _helloService = helloService;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = _helloService.SayHello("Mathieu");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            var result = await CallWcfAsync(_helloService, x => x.SayHello("mathieu"));
            label1.Text = result;
            button2.Enabled = true;
        }

        private Task<TResult> CallWcfAsync<TService, TResult>(TService service, Func<TService, TResult> call)
        {
            return Task<TResult>.Factory.FromAsync(service.BeginWcfCall(call), ar => service.EndWcfCall<TResult>(ar));
        }

        private Task CallWcfAsync<TService>(TService service, Action<TService> call)
        {
            return Task.Factory.FromAsync(service.BeginWcfCall(call), ar => service.EndWcfCall(ar));
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await AsyncErrorHandling(CallWcfAsync(_helloService, x => x.ThrowError()));
        }

        private async Task AsyncErrorHandling(Task task, Action<Exception> onException = null)
        {
            if(onException == null)
            {
                onException = OnError;
            }

            try
            {
                await task;
            }
            catch (Exception exc)
            {
                onException.Invoke(exc);
            }
        }

        private void OnError(Exception exc)
        {
            MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task<string>>();

            for (int i = 0; i < 400; i++)
            {
                tasks.Add(CallWcfAsync(_helloService, x => x.RandomLength()));
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                Debug.WriteLine(task.Result);
            }

            MessageBox.Show("done");
        }
    }
}
