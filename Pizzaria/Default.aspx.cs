﻿using System;
using System.Collections.Generic;
using System.Web.Services;
using Pizzaria.Dominio.Entidades;
using Pizzaria.Dominio.Repositorios;
using Pizzaria.Dominio.Servicos;
using Pizzaria.NHibernate.Helpers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Pizzaria.NHibernate.Repositorios;

namespace Pizzaria
{
    public partial class _Default : System.Web.UI.Page
    {
        private IAdministradorServico _administradorServico;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = Global.InicializarContainer();
            _administradorServico = container.Resolve<IAdministradorServico>();
            
            CarregaDropDownList();

            if (Request.QueryString["insertNewPizza"] != null)
            {
                InsertNewPizza();
                
                
            }
            
            if (Request.QueryString["insertNewIngrediente"] != null)
            {
                
                    InsertNewIngrediente();
                    
                
            }
        }


        private void CarregaDropDownList()
        {
            DropDownListIngrediente1.DataSource = _administradorServico.PesquisarIngredientes();
            DropDownListIngrediente2.DataSource = _administradorServico.PesquisarIngredientes();
            DropDownListIngrediente3.DataSource = _administradorServico.PesquisarIngredientes();

            DropDownListIngrediente1.DataBind();
            DropDownListIngrediente2.DataBind();
            DropDownListIngrediente3.DataBind();

        }
        
        [WebMethod]
        public static IList<Ingrediente> CarregaDropDownListAjax()
        {
            var container = Global.InicializarContainer();
            var administradorServico = container.Resolve<IAdministradorServico>();
            var ingredientes = administradorServico.PesquisarIngredientes();
            return ingredientes;
            
        }
        
        private void InsertNewPizza()
        {
                string nome = Request.Form["Nome"];
                int ingrediente1 = Convert.ToInt32(Request.Form["Ingrediente1"]);
                int ingrediente2 = Convert.ToInt32(Request.Form["Ingrediente2"]);
                int ingrediente3 = Convert.ToInt32(Request.Form["Ingrediente3"]);
                bool bordaRecheada = Convert.ToBoolean(Request.Form["Borda"]);
                _administradorServico.CriarPizza(nome, ingrediente1, ingrediente2, ingrediente3,bordaRecheada);
            
        }

        private void InsertNewIngrediente()
        {
            string ingrediente = Request.Form["Ingrediente"];

            _administradorServico.CriarIngrediente(ingrediente);
            

        }

        
    }
}
