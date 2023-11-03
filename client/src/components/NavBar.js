import { useEffect, useRef, useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {

  Collapse,
  Nav,
  NavLink,
  NavItem,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  
} from "reactstrap";
import { logout } from "../managers/authManager";
import Orders from "./order/Orders";
import {GiDeliveryDrone, GiAutoRepair} from 'react-icons/gi';
import {LiaRouteSolid} from 'react-icons/lia';
import {PiPackageDuotone} from 'react-icons/pi';
import LoginModal from "./auth/LoginModal";
import {  Button, Popover } from "react-bootstrap";
import "../App.css";


export default function NavBar({ loggedInUser, setLoggedInUser }) {
  const [open, setOpen] = useState(false);
  const toggleNavbar = () => setOpen(!open);

  return (
    <div>
      <Navbar color="warning" light fixed="true" expand="md" style={{ padding: "1rem", fontSize: "1.5rem", fontFamily: "'Inconsolata', monospace"}} >
        <NavbarBrand className="mr-auto" tag={RRNavLink} to="/" style={{  fontSize: "2rem"}}>
        <img
        alt="logo"
        src="https://www.insuranceprotector.co.uk/wp-content/uploads/2018/09/drone_sm.gif.pagespeed.ce_.pJe_-4ZbV8.gif"
        style={{
          height: 100,
          width: 100
        }}
      />SkyRoutes
        </NavbarBrand>
        {loggedInUser ? (
          <>
            <NavbarToggler onClick={toggleNavbar} />
            <Collapse isOpen={open} navbar>
              <Nav navbar>

                {loggedInUser.roles.includes("Admin") && (
                  <>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/orders">
                      <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><PiPackageDuotone /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Orders</p>                       
                        </div>
                      </div>                            
                      </NavLink>
                    </NavItem>

                    <NavItem>
                      <NavLink tag={RRNavLink} to="/drones">                      
                        <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><GiDeliveryDrone /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Drones </p>                       
                        </div>
                      </div>    
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/tickets">
                         
                        <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><GiAutoRepair /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Tickets </p>                       
                        </div>
                      </div> 
                      </NavLink>
                    </NavItem>
                  </>
                )}
                <NavItem>
                  <NavLink tag={RRNavLink} to="/routes">

                    <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><LiaRouteSolid /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Routes </p>                       
                        </div>
                      </div> 
                  </NavLink>
                </NavItem>
                {loggedInUser.roles.includes("Technician") && (
                  <>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/drones">
                      <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><GiDeliveryDrone /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Drones </p>                       
                        </div>
                      </div> 
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/tickets">
                      <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><GiAutoRepair /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Tickets </p>                       
                        </div>
                      </div> 
                      </NavLink>
                    </NavItem>
                  </>
                )}
                {loggedInUser.roles.includes("Expeditor") && (
                  <>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/orders">
                      <div class="popover__wrapper">
                        <a href="#">
                        <h2 class="popover__title"><PiPackageDuotone /></h2>                        
                        </a>
                        <div class="popover__content">
                          <p class="popover__message">Orders</p>                       
                        </div>
                      </div>   
                      </NavLink>
                    </NavItem>
                  </>
                )}
              </Nav>
            </Collapse>
            <Button
              color="dark"
              onClick={(e) => {
                e.preventDefault();
                setOpen(false);
                logout().then(() => {
                  setLoggedInUser(null);
                  setOpen(false);
                });
              }}
            >
              Logout
            </Button>
          </>
        ) : (
          <Nav navbar>
            <NavItem>
              <NavLink tag={RRNavLink} to="/login">
                <LoginModal setLoggedInUser={setLoggedInUser}/>
              </NavLink>
            </NavItem>
          </Nav>
        )}
      </Navbar>
    </div>
  );
}