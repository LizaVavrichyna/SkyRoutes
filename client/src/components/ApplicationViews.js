import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import Orders from "./order/Orders";
import Drones from "./drone/Drones";
import Tickets from "./ticket/Tickets";
import RouteList from "./route/RouteList";
import LandingPage from "./LandingPage";


export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <LandingPage loggedInUser={loggedInUser}/>
            </AuthorizedRoute>
          }
        />
        <Route path="/orders">
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <Orders loggedInUser={loggedInUser}/>
              </AuthorizedRoute>
            }/>
          
        </Route>
        <Route path="/routes">
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser} >
                <RouteList loggedInUser={loggedInUser}/>
              </AuthorizedRoute>
            }
          />
        </Route>

        <Route path="/drones">
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <Drones loggedInUser={loggedInUser}/>
              </AuthorizedRoute>
            } />
        </Route>

        <Route path='/tickets'>
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser} >
                <Tickets loggedInUser={loggedInUser}/>
              </AuthorizedRoute>
            }
          />
        </Route>
        
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
