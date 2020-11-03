import React, { Component } from "react";
import $ from 'jquery';
import './Empresa.css' 
import Settings from "../../settings.json"
import { IdNameModel } from "../../Model/IdNameModel";
import { ItemList } from "../ItemList/ItemList";
import { Method } from "@testing-library/react";

class Empresa extends Component {
  items : Array<IdNameModel>;
  navigate: any;
  constructor(props:any) {
    super(props);
    this.navigate = (this.props as any).navigate;
    this.items = new Array<IdNameModel>();
  }

  displayItems(items:Array<IdNameModel>){
    this.items = items;
    this.setState(this.items);
  }

  componentDidMount() {
    $.get(
      Settings.environment.baseURL +  "Empresa",
      this.displayItems.bind(this)
    ).catch(function(e)
    {
      
    });
  }
 
  render() {
    const props = {navigate: (this.props as any).navigate, items: this.items, screen: "AppScreen.EditEmpresa"};
    return (
      <div className="container-fullwidth">
        <div className="col-12">
          <ItemList {...props}/>
        </div>
      </div>
    );
  }
};
 
export default Empresa;