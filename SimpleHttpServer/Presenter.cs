using System.Diagnostics;

namespace SimpleHttpServer
{
    class Presenter
    {
        readonly Server _server = new Server();
        private readonly IView _view;

        public Presenter(IView view)
        {
            _view = view;
            Init();

            _view.StartClicked += _view_StartClicked;
        }

        private void _view_StartClicked(object sender, System.EventArgs e)
        {
            var ip = _view.GetIp();
            var port = _view.GetPort();

           var res= _server.Start(ip,port);

           _view.SetStartText(res ? "Dispose" : "Start");

           Process.Start($"http://{ip}:{port}");
        }

        public void Init()
        {
            var hosts = _server.GetHosts();

            _view.SetPort(80);
            _view.SetServerList(_server.GetHosts());
        }

        public void Run()
        {
            _view.ShowDialog();
        }
    }
}
