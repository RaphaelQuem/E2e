import React, { Component } from "react";
import { IdNameModel } from "../../Model/IdNameModel";
import './ItemList.css' 

export class ItemList extends Component {
  
  createItems(item: IdNameModel, navigate: any ) : JSX.Element{
    return (
      <li className="list-group" key={item.id}>{item.name}
        <a href="#" className="pull-right" onClick={navigate} data-navigate={(this.props as any).screen} data-id={item.id}>Editar</a>
      </li>
    )
  }  

  render() {
    var props = this.props as any;
    if(!props.items)
    {
      props = {navigate: props.navigate, items: new Array<IdNameModel>()} as any
    }
    return (
       <div>
         <ul className="list-group-item">
          {props.items.map((item: any) => this.createItems(item, props.navigate))}
         </ul>
       </div>
    );
  }
};