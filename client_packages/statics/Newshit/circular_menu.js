import React, { Component } from 'react';
import { connect } from 'react-redux';
import './css/circular_menu.css';

import circular_menu from './config/circular_menu.json';

//import mp from './mp';

class CircularMenu extends Component {
    constructor(props) {
        super(props)

        this.handleKeyUp = this.handleKeyUp.bind(this)

        window.setCircularEntity = this.props.setEntity
        window.setCircularFaction = this.props.setFaction
        window.setCircularSelectEntity = this.props.setSelectEntity
    }
    componentDidMount() {
        document.addEventListener("keyup", this.handleKeyUp);
    }
    componentWillUnmount() {
        document.removeEventListener("keyup", this.handleKeyUp)
    }

    handleKeyUp(event) {
        if(event.keyCode === 27 && this.props.gui === "circular_menu") {
            event.preventDefault();
            window.setGui(null)
        } 
    }

    setSelectSub(key) {
        let subitem = circular_menu.menu[this.props.circular_menu.select].submenu;
        if(subitem !== undefined) {
            let item = subitem[key];
            if(item.event !== undefined) {
                mp.trigger(item.event, this.props.circular_menu.select_id)
                window.setGui(null);
            }
        }
        let facitem = circular_menu.menu[this.props.circular_menu.select].factions;
        if(facitem !== undefined) {
            let item = facitem[this.props.circular_menu.faction];
            if(item === undefined) return;
            let button = item[key]
            if(button.event !== undefined) {
                mp.trigger(button.event, this.props.circular_menu.select_id)
                window.setGui(null);
            }
        }
    }

    setSelect(key){
        let item = circular_menu.menu[key];
        if(item === undefined) return;
        this.props.setSelect(item.submenu === undefined && item.factions === undefined ? -1 : key);
        if(item.event !== undefined) {
            mp.trigger(item.event, this.props.circular_menu.select_id)
            window.setGui(null);
        }
    }

    render() {
        return (
            <div id="circular">
			{
                this.props.gui === "circular_menu" ? 
                <div className="circular">
                    <div className="close" onClick={()=>{window.setGui(null)}}><i className="circular-icon ic_close"></i></div>
                    <div className="circular-sub-menu">
                        <ul>
                        {
                            this.props.circular_menu.select === -1 ? "" :                  
                            circular_menu.menu[this.props.circular_menu.select].factions !== undefined && circular_menu.menu[this.props.circular_menu.select].factions[this.props.circular_menu.faction] !== undefined ? circular_menu.menu[this.props.circular_menu.select].factions[this.props.circular_menu.faction].map((item, key) => {
                                return (
                                    <li key={key} className={item.id}><a onClick={() => {this.setSelectSub(key)}} ><div><span className="text">{item.name}</span></div></a></li>
                                )
                            }) : 
                            circular_menu.menu[this.props.circular_menu.select].submenu !== undefined? circular_menu.menu[this.props.circular_menu.select].submenu.map((item, key) => {
                                return (
                                    <li key={key} className={item.id}><a onClick={() => {this.setSelectSub(key)}} ><div><span className="text">{item.name}</span></div></a></li>
                                )
                            }) : ""
                        }
                        </ul>
                    </div>
                    <div className="circular-menu">
                        <ul>
                        {
                            circular_menu.menu.map((item, key) => {
                                return (
                                    <li key = {key} className={item.id}><a className={this.props.circular_menu.select === key ? "active" : ""} onClick={() => {this.setSelect(key)}} ><div>
                                        <span className={"circular-icon " + item.icon}></span>
                                        <span className="text">{item.name}</span>
                                    </div></a></li>
                                )
                            }) 
                        }
                        </ul>
                    </div>
                </div> : <div></div>
            }
            </div>
        )
    }
}

export default connect(
    state => ({
        circular_menu: state.circular_menu,
        gui: state.gui.open
    }),
    dispatch => ({
        setSelectEntity: (select_id, select_type) => {
            window.setGui("circular_menu");
            dispatch({ type: 'CIRCULAR_MENU_SELECT_ENTITY', select_id, select_type});
        },        
        setEntity: (entity_id, entity_type) => {
            dispatch({ type: 'CIRCULAR_MENU_ENTITY', entity_id, entity_type});
        },
        setSelect: (select) => {
            dispatch({ type: 'CIRCULAR_MENU_SELECT', select});
        },
        setFaction: (faction) => {
            dispatch({ type: 'CIRCULAR_MENU_FACTION', faction});
        },
    })
)(CircularMenu);