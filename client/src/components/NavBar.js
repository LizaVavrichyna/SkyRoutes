import { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Button,
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

export default function NavBar({ loggedInUser, setLoggedInUser }) {
  const [open, setOpen] = useState(false);

  const toggleNavbar = () => setOpen(!open);

  return (
    <div>
      <Navbar color="light" light fixed="true" expand="md" style={{ padding: "1rem", fontSize: "1.5rem"}} >
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
                        Orders <PiPackageDuotone />
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/drones">
                        Drones <GiDeliveryDrone />
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/tickets">
                        Tickets <GiAutoRepair />
                      </NavLink>
                    </NavItem>
                  </>
                )}
                <NavItem>
                  <NavLink tag={RRNavLink} to="/routes">
                    Routes <LiaRouteSolid />
                  </NavLink>
                </NavItem>
                {loggedInUser.roles.includes("Technician") && (
                  <>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/drones">
                        Drones <GiDeliveryDrone />
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/tickets">
                        Tickets <GiAutoRepair />
                      </NavLink>
                    </NavItem>
                  </>
                )}
                {loggedInUser.roles.includes("Expeditor") && (
                  <>
                    <NavItem>
                      <NavLink tag={RRNavLink} to="/orders">
                        Orders <PiPackageDuotone />
                      </NavLink>
                    </NavItem>
                  </>
                )}
              </Nav>
            </Collapse>
            <Button
              color="primary"
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
                <Button color="primary">Login</Button>
              </NavLink>
            </NavItem>
          </Nav>
        )}
      </Navbar>
    </div>
  );
}