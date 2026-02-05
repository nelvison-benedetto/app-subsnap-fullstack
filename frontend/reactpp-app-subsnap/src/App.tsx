import './styles/App.css'
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import Layout from './layouts/Layout';
import FeedPage from './pages/FeedPage';
import { Provider } from "react-redux";
import { store } from './app/store';
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from './query/queryClient';
import { useAuthListener } from './query/hooks/useAuthListener';
import TestSignUp from './pages/auth/TestSignUp';
import SignIn from './pages/auth/SignIn';
import { Toaster } from "react-hot-toast";
import SignUp from './pages/auth/SignUp';
import CompleteProfile from './pages/auth/CompleteProfile';
import WaitListPage from './pages/wait-list.page';

function App() {

  function CompUseAuthListener({ children }: { children: React.ReactNode }) {
    useAuthListener();
  return (<> {children} </>);
  }
  const waitlistPageEnabled = import.meta.env.VITE_WAITLISTPAGE_ENABLED === "true";
    //confronta la str di return di env var con 'true', result boolean saved in waitlistPageEnabled

  return (
    <>
      <Provider store={store}>
        <QueryClientProvider client={queryClient}>
          <CompUseAuthListener>

            <Toaster position="top-right" />  {/* ottima posizione per il Toaster! */}
            <BrowserRouter>
              <Routes>
                <Route element={<Layout />}>
                  
                  {waitlistPageEnabled && (  //su vercel questa env var è true, cosi tutti gli utenti possono accedere solo a WaitListPage
                    <>
                      <Route path="/waitlist" element={<WaitListPage />} />
                      <Route path="*" element={<Navigate to="/waitlist" replace />} />
                    </>
                  )}

                  {!waitlistPageEnabled && (  //su env.local questa env var è false, cosi su questo pc quando runno posso accedere a tutte le rotte
                    <>
                        <Route path="/" element={<FeedPage />} />

                        <Route path="/testauth" element={<TestSignUp />} />

                        <Route path="/auth">
                          <Route path="signin" element={<SignIn />} />  {/* i children sono paths relativi (dipendono dal father), quindi senza '/' davanti (che altrimenti diventano assoluti) */}
                          <Route path="signup" element={<SignUp />} />
                          <Route path="complete-profile" element={<CompleteProfile />} />
                        </Route>

                        <Route path="/waitlist" element={<WaitListPage />} />

                    </>
                  )}

                </Route>
              </Routes>
            </BrowserRouter>

          </CompUseAuthListener>
        </QueryClientProvider>
      </Provider>
    </>
  )
}

export default App
