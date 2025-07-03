import { BrowserRouter, Routes, Route, Navigate } from "react-router";
import LoginView from "./views/LoginView";
import RegisterView  from "./views/RegisterView";
import SavedUrlView  from "./views/SavedUrlView";
import  UrlView  from "./views/UrlView";




export default function Router(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<UrlView/>}/>
                <Route path="/login" element={<LoginView/>}/>
                <Route path="/register" element={<RegisterView/>}/>
                <Route path="/storage" element={<SavedUrlView/>}/>
            </Routes>
        </BrowserRouter>
    )
}