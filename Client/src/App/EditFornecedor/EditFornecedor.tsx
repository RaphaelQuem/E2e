import React, { Component } from "react";
import Settings from "../../settings.json";
import $, { valHooks } from 'jquery';
import { LinkedModelList } from "../../Model/EmpresaListFornecedorModel";
import { FornecedorModel } from "../../Model/FornecedorModel";
import DatePicker from 'react-datepicker';
 
import "react-datepicker/dist/react-datepicker.css";
import "./EditFornecedor.css";
class EditFornecedor extends Component {

  _id:number=0;
  _listEmpresa:LinkedModelList = new LinkedModelList();
  _fornecedor: FornecedorModel= new FornecedorModel();
  constructor(props: any){
    super(props);
    this._id = (this.props as any).id;
  }

  addLink(event: any){
    let empresaId = $(event.target).attr("data-id");

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Fornecedor/Link",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify({id: 0, idEmpresa: Number(empresaId), idFornecedor: this._id}),
      success: this.listEmpresaHandler.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }
  removeLink(event: any){

    let empresaId = $(event.target).attr("data-id");

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Fornecedor/Unlink",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify({id: 0, idEmpresa: Number(empresaId), idFornecedor: this._id}),
      success: this.listEmpresaHandler.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }

  submitSuccess(result: any){
    console.log('submited');
  }
  submitFail(){
    
  }
  onSubmit(event: any) {
    event.preventDefault();
    event.stopPropagation();

    let obj : any = {};
    
    obj.id = Number($('#f-id').val() ?? 0);
    obj.nome = $('#f-nome').val();

    obj.email = $('#f-email').val();
    obj.id = $('f-id').val();

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Fornecedor",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify(obj),
      success: this.submitSuccess.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }

  fornecedorHandler (result:FornecedorModel ) {
    this._fornecedor = result;
    this.setState(this._fornecedor);
  }
  getFornecedor() {
    $.get(
      Settings.environment.baseURL +  "Fornecedor/" + this._id,
      this.fornecedorHandler.bind(this)
    ).catch(function(e)
    {
      
    });
  }

  listEmpresaHandler(result:LinkedModelList) {
    this._listEmpresa = result;
    this.setState(this._listEmpresa);
  }
  getListEmpresa() {
    $.get(
      Settings.environment.baseURL +  "Fornecedor/ListEmpresa/" + this._id,
      this.listEmpresaHandler.bind(this)
    ).catch(function(e)
    {
      
    });
  }

  componentDidMount() {
    this.getFornecedor();
    this.getListEmpresa();
  }

  render() {
    return (
      <div>
        <div className="container">
          <form className="form" onSubmit={this.onSubmit.bind(this)}>
            <input type="hidden" id="f-id" value={this._id}></input>
            <input name="f-pessoafisica" type="radio" checked={this._fornecedor.pessoaFisica}>
            </input>
            <input name="f-pessoafisica" type="radio">
            </input>
            <input id="f-nome"
              type="text"
              placeholder="Nome"
              className="form-control"
              value={this._fornecedor.nome}
            />
            <input id="f-email"
              type="text"
              placeholder="Email"
              className="form-control"
              value={this._fornecedor.email}
            />
            <input id="f-documento"
              type="text"
              placeholder="CPF/CNPJ"
              className="form-control"
              value={this._fornecedor.documento}
            />
            <input id="f-rg"
              type="text"
              placeholder="RG"
              className="form-control"
              value={this._fornecedor.rg}
            />
            <DatePicker
                selected={this._fornecedor.nascimento}
                onChange={function(){}}
                name="startDate"
                dateFormat="dd/MM/yyyy"
                placeholderText="Nascimento">
            </DatePicker>
            <button className="btn btn-primary">
              Salvar
            </button>
          </form>
        </div>
        <div className="container">
          <div className="row">
            <div className="offset-md-1 col-12 col-md-4">
              {
                this._listEmpresa.notLinked.map(f =>
                  <div className="card">
                  <div className="card-body">
                    <span>f.name</span>
                    <span className="pull-right"><a href="#" onClick={this.addLink.bind(this)} data-id={f.id}>Adicionar</a></span>
                  </div>
                </div> 
                )
              }
            </div>
            <div className="col-12 col-md-1">

            </div>
            <div className="col-12 col-md-4">
              {
                this._listEmpresa.linked.map(f =>
                  <div className="card">
                  <div className="card-body">
                    <span>f.name</span>
                    <span className="pull-right"><a href="#" onClick={this.removeLink.bind(this)} data-id={f.id}>Remover</a></span>
                  </div>
                </div> 
                )
              }
            </div>  
          </div>
        </div>
      </div>
    );
  }
}

export default EditFornecedor;