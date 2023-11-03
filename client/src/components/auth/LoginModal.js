import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../managers/authManager";
import { Button, FormFeedback, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";


export default function LoginModal({ setLoggedInUser }) {

    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [failedLogin, setFailedLogin] = useState(false);
    const [modal, setModal] = useState(false);
  
    const toggle = () => setModal(!modal);
  
    const handleSubmit = (e) => {
      e.preventDefault();
      login(email, password).then((user) => {
        if (!user) {
          setFailedLogin(true);
        } else {
          setLoggedInUser(user);
          navigate("/");
        }
      });
    };
    return (
        <div>
        <Button color="danger" onClick={toggle}>
        Login
      </Button>
         <Modal isOpen={modal} toggle={toggle} >
        <ModalHeader toggle={toggle}>Welcome to SkyRoutes</ModalHeader>
        <ModalBody>
        <h3>Login</h3>
      <FormGroup>
        <Label>Email</Label>
        <Input
          invalid={failedLogin}
          type="text"
          value={email}
          onChange={(e) => {
            setFailedLogin(false);
            setEmail(e.target.value);
          }}
        />
      </FormGroup>
      <FormGroup>
        <Label>Password</Label>
        <Input
          invalid={failedLogin}
          type="password"
          value={password}
          onChange={(e) => {
            setFailedLogin(false);
            setPassword(e.target.value);
          }}
        />
        <FormFeedback>Login failed.</FormFeedback>
      </FormGroup>
      <Button color="primary" onClick={handleSubmit}>
        Login
      </Button>

        </ModalBody>
        <ModalFooter>


          <p>
        Not signed up? Register <Link to="/register">here</Link>
      </p>
        </ModalFooter>
      </Modal>
      </div>
    )
}