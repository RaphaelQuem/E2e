import React, { Component } from 'react';
import $ from 'jquery';
import './App.css';
import EditFornecedor from './EditFornecedor/EditFornecedor';
import Fornecedor from './Fonecedor/Fornecedor';
import { IApp } from './IApp';
import Empresa from './Empresa/Empresa';
import EditEmpresa from './EditEmpresa/EditEmpresa';

class App extends Component {
  _appState : IApp = {screen: 'AppScreen.Fornecedor', id: 0};
  constructor(props:any) {
    super(props);
  }
  onNavigateClick(event : any) {
    event.preventDefault();
    let to = $(event.target).attr("data-navigate") as string;
    let id = $(event.target).attr("data-id");
    this._appState.screen = to;
    this._appState.id = Number(id);
    this.setState(this._appState);
  }

  render() { 
    const props = {navigate: this.onNavigateClick.bind(this), id: this._appState.id};
    return (
    <div>
      <div className="navbar navbar-expand-lg navbar-light bg-dark w-100">
        <a href="#" data-navigate='AppScreen.Fornecedor' onClick={this.onNavigateClick.bind(this)}>
          Fornecedores
        </a>
        <a href="#" data-navigate='AppScreen.Empresa' onClick={this.onNavigateClick.bind(this)}>
          Empresa
        </a>
      </div>
      <div>
          {
            (this._appState.screen ===  'AppScreen.Fornecedor')?  
              <Fornecedor {...props}/>:
                (this._appState.screen ===  'AppScreen.Empresa')?  
                <Empresa {...props}/>:
                  (this._appState.screen ===  'AppScreen.EditEmpresa')?  
                  <EditEmpresa {...props}/>:
                    (this._appState.screen ===  'AppScreen.EditFornecedor')?  
                    <EditFornecedor {...props}/>: 
                    null
          }
      </div>
    </div>
    )};
}


export default App;
