import React, { Component } from "react";
import Settings from "../../settings.json";
import $, { valHooks } from 'jquery';
import "./EditEmpresa.css";
import { LinkedModelList } from "../../Model/EmpresaListFornecedorModel";
class EditEmpresa extends Component {

  _id:number=0;
  _listFornecedor:LinkedModelList = new LinkedModelList();
  constructor(props: any){
    super(props);
    this._id = (this.props as any).id;
  }

  addLink(event: any){
    let fornecedorId = $(event.target).attr("data-id");

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Empresa/Link",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify({id: 0, idEmpresa: this._id, idFornecedor: Number(fornecedorId)}),
      success: this.listFornecedorHandler.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }
  removeLink(event: any){

    let fornecedorId = $(event.target).attr("data-id");

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Empresa/Unlink",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify({id: 0, idEmpresa: this._id, idFornecedor: Number(fornecedorId)}),
      success: this.listFornecedorHandler.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }

  empresaHandler () {

  }
  getEmpresa() {
    $.get(
      Settings.environment.baseURL +  "Empresa/" + this._id,
      this.empresaHandler.bind(this)
    ).catch(function(e)
    {
      
    });
  }

  listFornecedorHandler(result:LinkedModelList) {
    this._listFornecedor = result;
    this.setState(this._listFornecedor);
  }
  getListFornecedor() {
    $.get(
      Settings.environment.baseURL +  "Empresa/ListFornecedor/" + this._id,
      this.listFornecedorHandler.bind(this)
    ).catch(function(e)
    {
      
    });
  }

  componentDidMount() {
    this.getEmpresa();
    this.getListFornecedor();
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
    obj.nomeFantasia = $('#f-nomefantasia').val();
    obj.cnpj = $('#f-cnpj').val();
    obj.UF = $('#f-uf').val();

    let postcfg: JQueryAjaxSettings = {
      url: Settings.environment.baseURL + "Empresa",
      contentType: "application/json",
      type: "POST",
      data: JSON.stringify(obj),
      success: this.submitSuccess.bind(this),
      error: this.submitFail.bind(this),
    };

    $.ajax(postcfg);
  }

  render() {
    return (
      <div>
        <div className="container">
          <form className="form" onSubmit={this.onSubmit.bind(this)}>
            <input type="hidden" id="f-id"></input>
            <input id="f-cnpj"
              type="text"
              placeholder="CNPJ"
              className="form-control"
            />
            <input id="f-nomefantasia"
              type="text"
              placeholder="nomefantasia"
              className="form-control"
            />
            <input id="f-uf"
              type="text"
              placeholder="UF"
              className="form-control"
            />
            <button className="btn btn-primary">
              Salvar
            </button>
          </form>
        </div>
        <div className="container">
          <div className="row">
            <div className="offset-md-1 col-12 col-md-4">
              {
                this._listFornecedor.notLinked.map(f =>
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
                this._listFornecedor.linked.map(f =>
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

export default EditEmpresa;