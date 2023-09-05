using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconMvvm.ViwModel
{
    public abstract class ViewModelBase<TModel> : ObservableRecipient
    {
        public TModel Model { get; set; }

        //public IMediator _mediator;


        public ViewModelBase()
        {
            //var builder = new ContainerBuilder();

            //var configBuilder = MediatRConfigurationBuilder.Create(typeof(ShowExcelViewModel).Assembly);
            //configBuilder.WithAllOpenGenericHandlerTypesRegistered();

            //builder.RegisterMediatR(configBuilder.Build());
            //builder.RegisterAssemblyModules(typeof(ShowExcelViewModel).Assembly);

            //var container = builder.Build();

            //_mediator = container.Resolve<IMediator>();
        }
    }
}
